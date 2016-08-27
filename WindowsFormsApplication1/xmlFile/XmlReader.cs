using Sanford.Multimedia.Midi.Config;
using SequencerDemo.Note;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApplication1.xmlFile
{
    public sealed class XmlReader
    {
        private BackgroundWorker loadWorker = new BackgroundWorker();

        private Score score = null;

        public Score Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }

        #region Events

        public event EventHandler<AsyncCompletedEventArgs> LoadCompleted;

        public event ProgressChangedEventHandler LoadProgressChanged;
        #endregion

        public XmlReader(string fileName, SequencerDemo.Note.Score score)
        {
            InitializeBackgroundWorkers();
            this.score = score;

            Load(fileName);
        }

        private void InitializeBackgroundWorkers()
        {
            loadWorker.DoWork += new DoWorkEventHandler(LoadDoWork);
            loadWorker.ProgressChanged += new ProgressChangedEventHandler(OnLoadProgressChanged);
            loadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnLoadCompleted);
            loadWorker.WorkerReportsProgress = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadDoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = (string)e.Argument;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode root = xmlDoc.SelectSingleNode("score-partwise");//查找﹤score-partwise﹥ 
        }

        private void OnLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EventHandler<AsyncCompletedEventArgs> handler = LoadCompleted;

            if (handler != null)
            {
                handler(this, new AsyncCompletedEventArgs(e.Error, e.Cancelled, null));
            }
        }

        private void OnLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChangedEventHandler handler = LoadProgressChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Loads a MIDI file into the Sequence.
        /// </summary>
        /// <param name="fileName">
        /// The MIDI file's name.
        /// </param>
        public void Load(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode root = xmlDoc.SelectSingleNode("score-partwise");//查找﹤score-partwise﹥
            XmlNode title = root.SelectSingleNode("//work/work-title");
            this.score.Name = title.InnerText;
            XmlNodeList measureList = root.SelectNodes("//part/measure");
            foreach(XmlNode item in measureList)
            {
                Measure measure = new Measure();
                TreatAttributesNode(item,measure);
                TreatNoteNode(item, measure);
                TreatBarlineNode(item, measure);
                this.score.AddMeasure(measure);
            }
        }

        /// <summary>
        /// 处理Attributes节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="measure"></param>
        private void TreatAttributesNode(XmlNode item,Measure measure)
        {
            XmlNode attributeNode = item.SelectSingleNode("//measure/attributes");
            if (attributeNode != null)
            {
                XmlNode timeNode = attributeNode.SelectSingleNode("time");
                if (timeNode != null)
                {
                    XmlNode beatsNode = timeNode.SelectSingleNode("beats");
                    if (beatsNode != null)
                    {
                        measure.Beats = int.Parse(beatsNode.InnerText);
                    }
                    XmlNode beatTypeNode = timeNode.SelectSingleNode("beat-type");
                    if (beatTypeNode != null)
                    {
                        measure.Beat_Type = int.Parse(beatTypeNode.InnerText);
                    }
                }
            }
        }

        private void TreatNoteNode(XmlNode item, Measure measure)
        { 
            XmlNodeList noteList = item.SelectNodes("note");
            if(noteList != null)
            {
                foreach (XmlNode noteItem in noteList)
                {
                    int staff = 0;
                    XmlNode staffNode = noteItem.SelectSingleNode("staff");
                    if (staffNode != null)
                    {
                        staff = int.Parse(staffNode.InnerText);
                    }
                    bool restFlag = false;
                    string key = string.Empty;
                    string type = string.Empty;
                    XmlNode typeNode = noteItem.SelectSingleNode("type");
                    if (typeNode != null)
                    {
                        type = typeNode.InnerText;
                    }

                    XmlNode restNode = noteItem.SelectSingleNode("rest");
                    if (restNode != null)
                    {
                        restFlag = true;
                        key = type;
                    }
                    else
                    {
                        string step = string.Empty;
                        XmlNode stepNode = noteItem.SelectSingleNode("pitch/step");
                        if (stepNode != null)
                        {
                            step = stepNode.InnerText;
                        }
                        string octave = string.Empty;
                        XmlNode octaveNode = noteItem.SelectSingleNode("pitch/octave");
                        if (octaveNode != null)
                        {
                            octave = octaveNode.InnerText;
                        }
                        key = $"{octave}{step}";
                    }
                    


                    Note note =  NoteScoreTable.Instance.GetNoteLocation(key, staff);
                    if (noteItem.Attributes["default-x"] != null)
                    {
                        string defaultX = noteItem.Attributes["default-x"].InnerText;
                        if (!string.IsNullOrEmpty(defaultX))
                        {
                            note.DefaultX = int.Parse(defaultX);
                        }
                    }

                    XmlAttribute defaultYNode = noteItem.Attributes["default-y"];
                    if (defaultYNode != null)
                    {
                        string defaultY = noteItem.Attributes["default-y"].InnerText;
                        if (string.IsNullOrEmpty(defaultY))
                        {
                            note.DefaultY = int.Parse(defaultY);
                        }
                    }
                    note.Staff = staff;


                    XmlNode accidentalNode = noteItem.SelectSingleNode("accidental");
                    if(accidentalNode != null)
                    {
                        string accidental = accidentalNode.InnerText;
                        switch(accidental)
                        {
                            case "sharp":
                                note.Lift = NoteLift.Sharp;
                                break;
                            case "flat":
                                note.Lift = NoteLift.Flat;
                                break;
                            case "natural":
                                note.Lift = NoteLift.Natural;
                                break;
                        }
                    }
                    else
                    {
                        note.Lift = NoteLift.None;
                    }

                    switch (type)
                    {
                        case "whole":
                            note.NoteType = restFlag ? NoteType.AllStop : NoteType.Whole;
                            break;
                        case "half":
                            note.NoteType = restFlag ? NoteType.MinimsStop : NoteType.Minims;
                            break;
                        case "quarter":
                            note.NoteType = restFlag ? NoteType.CrotchetsCStop : NoteType.CrotchetsC;
                            break;
                        case "eighth":
                            note.NoteType = restFlag ? NoteType.QuaversStop : NoteType.Quavers;
                            break;
                        case "16th":
                            note.NoteType = restFlag ? NoteType.DemiquaverStop : NoteType.Demiquaver;
                            break;
                        case "32nd":
                            note.NoteType = restFlag ? NoteType.DemisemiquaverStop : NoteType.Demisemiquaver;
                            break;
                    }

                    XmlNode stemNode = noteItem.SelectSingleNode("stem");
                    if(stemNode != null)
                    {
                        string stem = stemNode.InnerText;
                        switch(stem)
                        {
                            case "up":
                                note.CrochetType = CrochetType.Up;
                                break;
                            case "down":
                                note.CrochetType = CrochetType.Down;
                                break;
                        }
                    }

                    XmlNodeList beamList = noteItem.SelectNodes("beam");
                    if(beamList != null)
                    {
                        foreach(XmlNode beamNode in beamList)
                        {
                            if (beamNode.Attributes["number"] != null)
                            {
                                string number = beamNode.Attributes["number"].InnerText;
                                string content = beamNode.InnerText;
                                note.AddBeam(number, content);
                            }
                        }
                    }

                    measure.AddNote(note);
                }
            }
        }

        private void TreatBarlineNode(XmlNode item, Measure measure)
        {
            measure.BarLine = BarLineType.None;
            XmlNode attributeNode = item.SelectSingleNode("barline");
            if (attributeNode != null)
            {
                XmlNode styleNode = attributeNode.SelectSingleNode("bar-style");
                if (styleNode != null)
                {
                    string style = styleNode.InnerText;
                    switch(style)
                    {
                        case "regular":
                            measure.BarLine = BarLineType.Regular;
                            break;
                        case "dotted":
                            measure.BarLine = BarLineType.Dotted;
                            break;
                        case "Dashed":
                            measure.BarLine = BarLineType.Dashed;
                            break;
                        case "heavy":
                            measure.BarLine = BarLineType.Heavy;
                            break;
                        case "light-light":
                            measure.BarLine = BarLineType.DobuleLight;
                            break;
                        case "light-heavy":
                            measure.BarLine = BarLineType.LightHeavy;
                            break;
                        case "heavy-light":
                            measure.BarLine = BarLineType.HeavyLight;
                            break;
                        case "heavy-heavy":
                            measure.BarLine = BarLineType.DobuleHeavy;
                            break;
                        case "tick":
                            measure.BarLine = BarLineType.Tick;
                            break;
                        case "short":
                            measure.BarLine = BarLineType.Short;
                            break;
                    }
                }
            }
        }
    }
}
