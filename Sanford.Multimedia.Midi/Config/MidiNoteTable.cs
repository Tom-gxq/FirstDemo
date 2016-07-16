using System;
using System.Collections.Generic;
using System.Text;

namespace Sanford.Multimedia.Midi.Config
{
    public class MidiNoteTable
    {
        private Dictionary<int, string> dic = new Dictionary<int, string>();
        private MidiNoteTable()
        {
            dic.Add(60, "Note1");
            dic.Add(65, "Note4");
            dic.Add(67, "Note5");
            dic.Add(69, "Note6");
            dic.Add(70, "Note7");
            dic.Add(72, "NoteDot1");
            dic.Add(74, "NoteDot2");
        }

        private static MidiNoteTable _instance;

        public static MidiNoteTable Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MidiNoteTable();
                }
                return _instance;
            }
        }

        public string GetNote(int key)
        {
            return dic[key];
        }
    }
}
