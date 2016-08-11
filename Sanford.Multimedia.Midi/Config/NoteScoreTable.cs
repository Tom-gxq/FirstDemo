using System;
using System.Collections.Generic;
using System.Text;

namespace Sanford.Multimedia.Midi.Config
{
    public enum SoundDepart
    {
        High,
        Center,
        Slow,
        Other
    }

    public enum StopNoteVal
    {
        AllStop = -1,
        MinimsStop = -2,
        CrotchetsCStop = -3,
        QuaversStop = -4
    }
    public struct NoteLocation
    {
        public SoundDepart soundType;
        public int line;
        public int offset;
    }
    public class NoteScoreTable
    {
        private Dictionary<int, SequencerDemo.Note.Note> dic = new Dictionary<int, SequencerDemo.Note.Note>();

        private List<SequencerDemo.Note.Note> blackList = new List<SequencerDemo.Note.Note>();

        private List<SequencerDemo.Note.Note> lowkList = new List<SequencerDemo.Note.Note>();
        private NoteScoreTable()
        {
            NoteLocation location = new NoteLocation (){ line=0, offset  = 0 ,soundType = SoundDepart.Center };
            SequencerDemo.Note.Note note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 60;
            dic.Add(note.Data, note);

            #region 高音部
            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.Center };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 62;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 1, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 64;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 1, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 65;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 2, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 67;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 2, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 69;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 3, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 71;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 3, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 72;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 74;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 4, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 76;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 5, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 77;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 5, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 79;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 81;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 83;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 84;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 86;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 88;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 89;
            dic.Add(note.Data, note);
            #endregion

            #region 低音部
            location = new NoteLocation() { line = 5, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 59;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 5, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 57;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 4, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 55;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 53;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 3, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 52;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 3, offset =0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 50;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 2, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 48;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 2, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 47;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 45;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 43;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 41;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 40;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 38;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 36;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 35;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -2, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 33;
            dic.Add(note.Data, note);
            lowkList.Add(note);
            #endregion

            #region 黑键

            #region 低音

            location = new NoteLocation() { line = -2, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 34;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = -1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 37;
            dic.Add(note.Data, note);
            blackList.Add(note);


            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 39;
            dic.Add(note.Data, note);
            blackList.Add(note);


            location = new NoteLocation() { line = 0, offset = 5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 42;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 44;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 1, offset = 5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 46;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 2, offset = 5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 49;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 3, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 51;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 54;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 4, offset = 5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 56;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 5, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 58;
            dic.Add(note.Data, note);
            blackList.Add(note);

            #endregion

            #region 高音
            location = new NoteLocation() { line = 0, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 61;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 1, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 63;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 1, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 66;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 2, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 68;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 3, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 70;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 3, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 73;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 4, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 75;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 78;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 5, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 80;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 82;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 7, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 85;
            dic.Add(note.Data, note);
            blackList.Add(note);

            location = new NoteLocation() { line = 7, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 87;
            dic.Add(note.Data, note);
            blackList.Add(note);

            #endregion

            #endregion

            #region 休止符
            //全休止符
            location = new NoteLocation() { line = 3, offset = -9, soundType = SoundDepart.Other };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = -1;
            note.NoteType = SequencerDemo.Note.NoteType.AllStop;
            dic.Add(note.Data, note);

            //二分休止符
            location = new NoteLocation() { line = 3, offset = -3, soundType = SoundDepart.Other };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = -2;
            note.NoteType = SequencerDemo.Note.NoteType.MinimsStop;
            dic.Add(note.Data, note);

            //四分休止符
            location = new NoteLocation() { line = 2, offset = 0, soundType = SoundDepart.Other };
            var endLocation = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.Other };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.EndLocation = endLocation;
            note.Data = -3;
            note.NoteType = SequencerDemo.Note.NoteType.CrotchetsCStop;
            dic.Add(note.Data, note);

            //八分休止符
            location = new NoteLocation() { line = 2, offset = -5, soundType = SoundDepart.Other };
            endLocation = new NoteLocation() { line = 3, offset = -5, soundType = SoundDepart.Other };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.EndLocation = endLocation;
            note.Data = -4;
            note.NoteType = SequencerDemo.Note.NoteType.QuaversStop;
            dic.Add(note.Data, note);
            #endregion
        }
        private static NoteScoreTable _instance;
        
        public static NoteScoreTable Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new NoteScoreTable();
                }
                return _instance;
            }
        }

        public SequencerDemo.Note.Note GetNoteLocation(int key)
        {
            if (dic.ContainsKey(key))
            {
                return (SequencerDemo.Note.Note)dic[key].Clone();
            }
            else
            {
                return null;
            }
        }

        public bool IsBlackNote(SequencerDemo.Note.Note note )
        {
            if(note == null)
            {
                return false;
            }
            var noteobj=  this.blackList.Find(x =>
            {
                return (x.Data == note.Data);
            });
            return (noteobj != null);
        }

        public bool IsLowNote(int data)
        {
            var noteobj = this.lowkList.Find(x =>
            {
                return (x.Data == data);
            });
            return (noteobj != null);
        }

    }
}
