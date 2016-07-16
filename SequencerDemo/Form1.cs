using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;
using Sanford.Multimedia.Midi.Config;
using SequencerDemo.Note;

namespace SequencerDemo
{
    public partial class Form1 : Form
    {
        private bool scrolling = false;

        private bool playing = false;

        private bool closing = false;

        private OutputDevice outDevice;

        private int outDeviceID = 0;
        private SequencerDemo.Note.Score score = null;

        private OutputDeviceDialog outDialog = new OutputDeviceDialog();

        public Form1()
        {
            InitializeComponent();            
        }

        protected override void OnLoad(EventArgs e)
        {
            if(OutputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);

                    sequence1.LoadProgressChanged += HandleLoadProgressChanged;
                    sequence1.LoadCompleted += HandleLoadCompleted;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Close();
                }
            }
            base.OnLoad(e);
        }

        
        protected override void OnPaint(PaintEventArgs e)//重写 
        {
            Graphics g = pictureBox1.CreateGraphics();

            using (Pen myPen = new Pen(Color.Black, 2))
            {
                int len = (ClientRectangle.Height-5) / 60;
                int tmp = 1;
                for (int i=1;i<=len;i++)
                {
                    int alllen = (tmp + 5);
                    for (int y =tmp; y < alllen; y++, tmp++)
                    {
                        g.DrawLine(myPen, new Point(0, y*10), new Point(ClientRectangle.Width, y*10));
                    }

                    tmp++;
                }

            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            pianoControl1.PressPianoKey(e.KeyCode);

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            pianoControl1.ReleasePianoKey(e.KeyCode);

            base.OnKeyUp(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            closing = true;

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            sequence1.Dispose();

            if(outDevice != null)
            {
                outDevice.Dispose();
            }

            outDialog.Dispose();

            base.OnClosed(e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openMidiFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openMidiFileDialog.FileName;

                try
                {
                    sequencer1.Stop();
                    playing = false;
                    this.score = new  SequencerDemo.Note.Score();
                    sequence1.LoadAsync(fileName, this.score);
                    this.Cursor = Cursors.WaitCursor;
                    startButton.Enabled = false;
                    continueButton.Enabled = false;
                    stopButton.Enabled = false;
                    openToolStripMenuItem.Enabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void outputDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();

            dlg.ShowDialog();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                playing = false;
                sequencer1.Stop();
                timer1.Stop();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                playing = true;
                sequencer1.Start();
                timer1.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            try
            {
                playing = true;
                sequencer1.Continue();
                timer1.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void positionHScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if(e.Type == ScrollEventType.EndScroll)
            {
                sequencer1.Position = e.NewValue;

                scrolling = false;
            }
            else
            {
                scrolling = true;
            }
        }

        private void HandleLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            startButton.Enabled = true;
            continueButton.Enabled = true;
            stopButton.Enabled = true;
            openToolStripMenuItem.Enabled = true;
            toolStripProgressBar1.Value = 0;

            if(e.Error == null)
            {
                positionHScrollBar.Value = 0;
                positionHScrollBar.Maximum = sequence1.GetLength();
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if(closing)
            {
                return;
            }
            //List<int> array = new List<int>() {60, 65,67,69,70,72,74};
            outDevice.Send(e.Message);
            //if (array.Contains(e.Message.Data1)&&(e.Message.Command == ChannelCommand.NoteOn))
            //{
            //    DrawNote(e.Message.Data1);
            //}
            this.score.AddNote(e.Message.Data1, e.Message.Command, e.Message.Ticks);
            pianoControl1.Send(e.Message);
        }
        private int locationX = 0;//音符x坐标基准点
        private int locationY = 0;//音符y坐标基准点
        private int barLocationY = 0;//音符y坐标基准点
        private int noteCount =0;//每行音符数
        private int noteBarCount = 0;//每行音符数
        private int barCount = 0;//小节数
        private const int TOTAL_LINE = 6;//五线谱总共有多少条线
        private const int LINE_HEIGHT_PER = 10;//五线谱每条线的高度
        private const int NOTE_VERTICAL_SPACING = 20;//每个音符的行间距
        private const int NOTE_TAIL_HEIGHT = LINE_HEIGHT_PER*2+ LINE_HEIGHT_PER/2;//音符尾巴高度

        /// <summary>
        // 描画音符*
        /// </summary>
        /// <param name="data"></param>
        private void DrawNote(int data)
        {
            noteCount++;
            //var noteName = MidiNoteTable.Instance.GetNote(data);
            var note = NoteScoreTable.Instance.GetNoteLocation(data);
            if(note == null)
            {
                return;
            }
            int pointY = note.Location.line - TOTAL_LINE;
            
            if (pointY != 0)
            {
                pointY = (-pointY + locationY)* LINE_HEIGHT_PER + note.Location.offset -4;
            }
            int pointX = locationX+ noteCount* NOTE_VERTICAL_SPACING;

            Graphics g = pictureBox1.CreateGraphics();
            using (Pen myPen = new Pen(Color.Red, 2))
            {
                g.DrawEllipse(myPen, pointX, pointY,4,5);
                Point p1 = new Point(pointX + 4, pointY);
                Point p2 = new Point(pointX + 4, pointY- NOTE_TAIL_HEIGHT);
                if (note.Location.line >= 3)
                {
                    p1 = new Point(pointX, pointY);
                    p2 = new Point(pointX, pointY + NOTE_TAIL_HEIGHT);
                }
                g.DrawLine(myPen, p1, p2);
                
                if(noteCount%4 == 0)
                {
                    using (Pen pen = new Pen(Color.Blue, 2))
                    {                        
                        Point pp1 = new Point(pointX + NOTE_VERTICAL_SPACING, locationY * LINE_HEIGHT_PER);
                        Point pp2 = new Point(pointX+ NOTE_VERTICAL_SPACING, locationY * LINE_HEIGHT_PER + 5* LINE_HEIGHT_PER);                        
                        g.DrawLine(pen,pp1,pp2);
                        locationX += NOTE_VERTICAL_SPACING;
                    }
                }
                if(noteCount == 14 )
                {
                    locationY += TOTAL_LINE;
                    locationX = 0;
                    noteCount = 0;
                }
            }
            
        }

        private void HandleChased(object sender, ChasedEventArgs e)
        {
            foreach(ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
       //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach(ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
                pianoControl1.Send(message);
            }
        }

        private void HandlePlayingCompleted(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void pianoControl1_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            #region Guard

            if(playing)
            {
                return;
            }

            #endregion

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }

        private void pianoControl1_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            #region Guard

            if(playing)
            {
                return;
            }

            #endregion

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!scrolling)
            {
                positionHScrollBar.Value = sequencer1.Position;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = this.score.NoteBars;
            if(list != null)
            {
                SequencerDemo.Note.Note lastNote = null;
                for (int i= list.Count-1; i>=0;i--)
                {
                    if(i >= 0)
                    {                        
                        DrawBar(lastNote);
                    }
                    if ((list[i] != null)&&(list[i].Notes != null))
                    {
                        for(int j= list[i].Notes.Count-1;j>=0;j--)
                        {
                            var block  = list[i].Notes[j];
                            if ((block != null)&&(block.Notes != null))
                            {
                                foreach(var note in block.Notes)
                                {
                                    lastNote = note;
                                    if ((note.NoteType >= NoteType.AllStop) && (note.NoteType <= NoteType.QuaversStop))
                                    {
                                        DrawStopNote(note);
                                    }
                                    else
                                    {
                                        DrawNote(note);
                                    }
                                }
                                if ((block.Notes.Count > 1)||(lastNote.NoteType >= NoteType.AllStop))
                                {
                                    DrawSymbolBar(block);
                                }
                                else
                                {
                                    DrawSymbolNote(lastNote);
                                }
                            }                            
                            
                        }

                    }

                    //最后一小节，时间不够的话，终止符代替
                    if (i == 0)
                    {
                        var lastBar = list[i];
                        double time = this.score.FewShot * this.score.BitTime - lastBar.BarTicks;
                        if (time > 0)
                        {
                            double paije = this.score.BitTime / time;
                            var stopNote = new SequencerDemo.Note.Note();
                            if (paije == 1)
                            {
                                //四分终止符         
                                stopNote = NoteScoreTable.Instance.GetNoteLocation((int)StopNoteVal.CrotchetsCStop);
                            }
                            else if (paije == 2)
                            {
                                //八分终止符
                                stopNote = NoteScoreTable.Instance.GetNoteLocation((int)StopNoteVal.QuaversStop);
                            }
                            else if (paije == 0.5)
                            {
                                //二分终止符
                                stopNote = NoteScoreTable.Instance.GetNoteLocation((int)StopNoteVal.MinimsStop);
                            }
                            else if (paije == 0.4)
                            {
                                //全终止符
                                stopNote = NoteScoreTable.Instance.GetNoteLocation((int)StopNoteVal.AllStop);
                            }
                            DrawStopNote(stopNote);
                        }
                    }

                    this.barCount++;
                }
            }
        }

        private void DrawNote(SequencerDemo.Note.Note note)
        {
            noteCount++;
            if (note == null)
            {
                return;
            }
            int pointY = note.Location.line - TOTAL_LINE;

            if (pointY != 0)
            {
                pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset - 4;
            }
            int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

            Graphics g = pictureBox1.CreateGraphics();
            using (Pen myPen = new Pen(Color.Red, 2))
            {
                //画符头
                g.DrawEllipse(myPen, pointX, pointY, 4, 5);
                //画符杆
                Point p1 = new Point(pointX + 4, pointY);
                Point p2 = new Point(pointX + 4, pointY - NOTE_TAIL_HEIGHT);
                if (note.CrochetType == CrochetType.Down)
                {
                    p1 = new Point(pointX, pointY);
                    p2 = new Point(pointX, pointY + NOTE_TAIL_HEIGHT);
                }
                g.DrawLine(myPen, p1, p2);
                
            }
        }

        /// <summary>
        /// 画终止符
        /// </summary>
        /// <param name="note"></param>
        private void DrawStopNote(SequencerDemo.Note.Note note)
        {
            if (note == null)
            {
                return;
            }
            switch (note.NoteType)
            {
                case NoteType.AllStop:
                    {
                        noteCount = 2;

                        int pointY = note.Location.line - TOTAL_LINE;

                        if (pointY != 0)
                        {
                            pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset;
                        }
                        int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

                        Graphics g = pictureBox1.CreateGraphics();
                        using (Pen myPen = new Pen(Color.Red, 2))
                        {
                            Point p1 = new Point(pointX, pointY);
                            Point p2 = new Point(pointX + 10, pointY);
                            g.DrawLine(myPen, p1, p2);
                        }

                        noteCount = 4;
                        this.noteBarCount = 3;
                    }
                    break;
                case NoteType.QuaversStop:
                    {
                        noteCount++;
                        int pointY = 3 - TOTAL_LINE;

                        if (pointY != 0)
                        {
                            pointY = (-pointY + locationY) * LINE_HEIGHT_PER - 5;
                        }
                        int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

                        Graphics g = pictureBox1.CreateGraphics();
                        using (Pen myPen = new Pen(Color.Red, 2))
                        {
                            g.DrawEllipse(myPen, pointX, pointY-3, 4, 5);                            

                            RectangleF oval = new RectangleF((float)pointX + 5, pointY,3,5);
                            g.DrawArc(myPen, oval, 180, -270);

                            Point p1 = new Point(pointX + 8, pointY-3);
                            Point p2 = new Point(pointX + 4, pointY + 15);
                            g.DrawLine(myPen, p1, p2);
                        }
                    }
                    break;

            }

        }

        /// <summary>
        /// 计算符杆的终点位置
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        private Point GetBarEndPoint(SequencerDemo.Note.Note note)
        {
            if (note == null)
            {
                return new Point();
            }
            int pointY = note.Location.line - TOTAL_LINE;

            if (pointY != 0)
            {
                pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset - 4;
            }
            int pointX = locationX + this.noteBarCount * NOTE_VERTICAL_SPACING;

            Point p = new Point(pointX + 4, pointY - NOTE_TAIL_HEIGHT);
            if (note.CrochetType == CrochetType.Down)
            {
                p = new Point(pointX, pointY + NOTE_TAIL_HEIGHT);
            }
            return p;
        }

        /// <summary>
        /// 画block的符杠
        /// </summary>
        /// <param name="block"></param>
        private void DrawSymbolBar(SequencerDemo.Note.NoteBlock block)
        {
            var headNote = block.Notes[0];
            var tailNote = block.GetLast();
            this.noteBarCount += 1;
            Point p1 = GetBarEndPoint(headNote);
            this.noteBarCount += (block.Notes.Count - 1);
            Point p2 = GetBarEndPoint(tailNote);
        
            Graphics g = pictureBox1.CreateGraphics();
            using (Pen myPen = new Pen(Color.Red, 2))
            {
                //画符杠
                switch (headNote.NoteType)
                {
                    case NoteType.Quavers:
                        g.DrawLine(myPen, p1, p2);
                        break;
                    case NoteType.Demiquaver:
                        g.DrawLine(myPen, p1, p2);
                        Point p4 = new Point();
                        Point p5 = new Point();
                        if (headNote.CrochetType == CrochetType.Down)
                        {
                            p4 = new Point(p1.X, p1.Y - 5);
                            p5 = new Point(p2.X, p2.Y - 5);
                        }
                        else
                        {
                            p4 = new Point(p1.X, p1.Y + 5);
                            p5 = new Point(p2.X, p2.Y + 5);
                        }
                        g.DrawLine(myPen, p4, p5);
                        break;
                }
            }
        }

        /// <summary>
        /// 画音符的符杠
        /// </summary>
        /// <param name="block"></param>
        private void DrawSymbolNote(SequencerDemo.Note.Note note)
        {
            if (note == null)
            {
                return;
            }
            this.noteBarCount += 1;
            int pointY = note.Location.line - TOTAL_LINE;

            if (pointY != 0)
            {
                pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset - 4;
            }
            int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

            //计算符杆的终点位置
            Point p = new Point(pointX + 4, pointY - NOTE_TAIL_HEIGHT);
            if (note.CrochetType == CrochetType.Down)
            {
                p = new Point(pointX, pointY + NOTE_TAIL_HEIGHT);
            }

            Graphics g = pictureBox1.CreateGraphics();
            using (Pen myPen = new Pen(Color.Red, 2))
            {
                Point p1 = new Point();
                Point p2 = new Point();
                Point p3 = new Point();
                Point p4 = new Point();
                Point p5 = new Point();
                //画符尾
                switch (note.NoteType)
                {
                    //画一条符尾
                    case NoteType.Quavers:
                        p1 = new Point(p.X+8, p.Y);
                        g.DrawLine(myPen, p, p1);
                        break;
                    //画两条符尾
                    case NoteType.Demiquaver:
                        p1 = new Point(p.X + 8, p.Y);
                        g.DrawLine(myPen, p, p1);
                        p2 = new Point(p.X,p.Y+4);
                        p3 = new Point(p2.X + 8, p2.Y);                        
                        g.DrawLine(myPen, p2, p3);
                        break;
                    //画三条符尾
                    case NoteType.Demisemiquaver:
                        p1 = new Point(p.X + 8, p.Y);
                        g.DrawLine(myPen, p, p1);
                        p2 = new Point(p.X, p.Y + 4);
                        p3 = new Point(p2.X + 8, p2.Y);
                        g.DrawLine(myPen, p2, p3);
                        p4 = new Point(p2.X, p2.Y + 4);
                        p5 = new Point(p4.X + 8, p4.Y);
                        g.DrawLine(myPen, p4, p5);
                        break;
                }
            }
        }

        /// <summary>
        /// 画小节线
        /// </summary>
        /// <param name="note"></param>
        private void DrawBar(SequencerDemo.Note.Note note)
        {
            if (note == null)
            {
                return;
            }
            int pointY = note.Location.line - TOTAL_LINE;

            if (pointY != 0)
            {
                pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset - 4;
            }
            int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

            Graphics g = pictureBox1.CreateGraphics();
            using (Pen pen = new Pen(Color.Blue, 2))
            {
                Point pp1 = new Point(pointX + NOTE_VERTICAL_SPACING, locationY * LINE_HEIGHT_PER);
                Point pp2 = new Point(pointX + NOTE_VERTICAL_SPACING, locationY * LINE_HEIGHT_PER + 5 * LINE_HEIGHT_PER);
                g.DrawLine(pen, pp1, pp2);
                locationX += NOTE_VERTICAL_SPACING;
            }

            if ((this.barCount > 0) && (this.barCount % 5 == 0))
            {
                locationY += TOTAL_LINE;
                locationX = 0;
                noteCount = 0;
                this.noteBarCount = 0;
            }
        }


    }
}