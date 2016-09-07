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
        private Dictionary<string, int> hightNoteValueDic = new Dictionary<string, int>();
        private Dictionary<string, int> lowNoteValueDic = new Dictionary<string, int>();

        private List<SequencerDemo.Note.Note> blackList = new List<SequencerDemo.Note.Note>();

        private List<SequencerDemo.Note.Note> lowkList = new List<SequencerDemo.Note.Note>();
        private NoteScoreTable()
        {
            NoteLocation location = new NoteLocation (){ line=0, offset  = 0 ,soundType = SoundDepart.Center };
            SequencerDemo.Note.Note note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 60;
            dic.Add(note.Data, note);

            #region 高音部的低音
            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 290;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 310;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -2, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 330;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 350;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 360;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 380;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 400;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 410;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 430;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 450;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 2, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 470;
            dic.Add(note.Data, note);
            lowkList.Add(note);            

            location = new NoteLocation() { line = 2, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 480;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 3, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 500;
            dic.Add(note.Data, note);
            lowkList.Add(note);
            
            location = new NoteLocation() { line = 3, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 520;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 530;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 590;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = -1, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 570;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = -1, offset = 5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 550;
            dic.Add(note.Data, note);
            #endregion

            #region 高音部
            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.High };
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

            location = new NoteLocation() { line = 5, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 91;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 93;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 95;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 96;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 98;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 100;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 101;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 5, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 103;
            note.Octave = 2;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 105;
            note.Octave = 2;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = -5, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 107;
            note.Octave = 2;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 108;
            note.Octave = 2;
            dic.Add(note.Data, note);
            #endregion

            #region 低音部的高音
            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 600;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 620;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 640;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 650;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 670;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 2, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 690;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 3, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 710;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 3, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 720;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 4, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 740;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 4, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 760;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 5, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 770;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 5, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 790;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 810;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 830;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 840;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 860;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 880;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 8, offset = 5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 890;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 5, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 910;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = 0, soundType = SoundDepart.High };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 930;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 6, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 950;
            note.Octave = 1;
            dic.Add(note.Data, note);

            location = new NoteLocation() { line = 7, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 980;
            note.Octave = 1;
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

            location = new NoteLocation() { line = 1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 31;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 29;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = 0, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 28;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 26;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -1, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 24;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -2, offset = -5, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 23;
            note.Octave = 1;
            dic.Add(note.Data, note);
            lowkList.Add(note);

            location = new NoteLocation() { line = -2, offset = 0, soundType = SoundDepart.Slow };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = 21;
            note.Octave = 1;
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

            //16分休止符
            location = new NoteLocation() { line = 3, offset = -3, soundType = SoundDepart.Other };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = -5;
            note.NoteType = SequencerDemo.Note.NoteType.MinimsStop;
            dic.Add(note.Data, note);

            //32分休止符
            location = new NoteLocation() { line = 3, offset = -3, soundType = SoundDepart.Other };
            note = new SequencerDemo.Note.Note();
            note.Location = location;
            note.Data = -6;
            note.NoteType = SequencerDemo.Note.NoteType.MinimsStop;
            dic.Add(note.Data, note);
            #endregion

            #region 高音音值    
            hightNoteValueDic.Add("1F", 290);
            hightNoteValueDic.Add("1G", 310);
            hightNoteValueDic.Add("1A", 330);
            hightNoteValueDic.Add("1B", 350);
            hightNoteValueDic.Add("2C", 360);
            hightNoteValueDic.Add("2D", 380);
            hightNoteValueDic.Add("2E", 400);
            hightNoteValueDic.Add("2F", 410);
            hightNoteValueDic.Add("2G", 430);
            hightNoteValueDic.Add("2A", 450);
            hightNoteValueDic.Add("2B", 470);
            hightNoteValueDic.Add("3C", 480);
            hightNoteValueDic.Add("3D", 500);
            hightNoteValueDic.Add("3E", 520);
            hightNoteValueDic.Add("3F", 530);
            hightNoteValueDic.Add("3G", 550);
            hightNoteValueDic.Add("3A", 570);
            hightNoteValueDic.Add("3B", 590);

            hightNoteValueDic.Add("4C",60);
            hightNoteValueDic.Add("4D", 62);
            hightNoteValueDic.Add("4E", 64);
            hightNoteValueDic.Add("4F", 65);
            hightNoteValueDic.Add("4G", 67);
            hightNoteValueDic.Add("4A", 69);
            hightNoteValueDic.Add("4B", 71);
            hightNoteValueDic.Add("5C", 72);
            hightNoteValueDic.Add("5D", 74);
            hightNoteValueDic.Add("5E", 76);
            hightNoteValueDic.Add("5F", 77);
            hightNoteValueDic.Add("5G", 79);
            hightNoteValueDic.Add("5A", 81);
            hightNoteValueDic.Add("5B", 83);
            hightNoteValueDic.Add("6C", 84);
            hightNoteValueDic.Add("6D", 86);
            hightNoteValueDic.Add("6E", 88);
            hightNoteValueDic.Add("6F", 89);
            hightNoteValueDic.Add("6G", 91);
            hightNoteValueDic.Add("6A", 93);
            hightNoteValueDic.Add("6B", 95);
            hightNoteValueDic.Add("7C", 96);
            hightNoteValueDic.Add("7D", 98);
            hightNoteValueDic.Add("7E", 100);
            hightNoteValueDic.Add("7F", 103);
            hightNoteValueDic.Add("7G", 105);
            hightNoteValueDic.Add("7A", 107);
            hightNoteValueDic.Add("7B", 108);
            hightNoteValueDic.Add("whole", -1);
            hightNoteValueDic.Add("half", -2);
            hightNoteValueDic.Add("quarter", -3);
            hightNoteValueDic.Add("eighth", -4);
            hightNoteValueDic.Add("16th", -5);
            hightNoteValueDic.Add("32nd", -6);
            #endregion

            #region 低音音值
            lowNoteValueDic.Add("4C", 600);
            lowNoteValueDic.Add("4D", 620);
            lowNoteValueDic.Add("4E", 640);
            lowNoteValueDic.Add("4F", 650);
            lowNoteValueDic.Add("4G", 670);
            lowNoteValueDic.Add("4A", 690);
            lowNoteValueDic.Add("4B", 710);
            lowNoteValueDic.Add("5C", 720);
            lowNoteValueDic.Add("5D", 740);
            lowNoteValueDic.Add("5E", 760);
            lowNoteValueDic.Add("5F", 770);
            lowNoteValueDic.Add("5G", 790);
            lowNoteValueDic.Add("5A", 810);
            lowNoteValueDic.Add("5B", 830);
            lowNoteValueDic.Add("6C", 840);
            lowNoteValueDic.Add("6D", 860);
            lowNoteValueDic.Add("6E", 880);
            lowNoteValueDic.Add("6F", 890);
            lowNoteValueDic.Add("6G", 910);
            lowNoteValueDic.Add("6A", 930);
            lowNoteValueDic.Add("6B", 950);
            lowNoteValueDic.Add("7D", 980);

            lowNoteValueDic.Add("3B", 59);
            lowNoteValueDic.Add("3A", 57);
            lowNoteValueDic.Add("3G", 55);
            lowNoteValueDic.Add("3F", 53);
            lowNoteValueDic.Add("3E", 52);
            lowNoteValueDic.Add("3D", 50);
            lowNoteValueDic.Add("3C", 48);
            lowNoteValueDic.Add("2B", 47);
            lowNoteValueDic.Add("2A", 45);
            lowNoteValueDic.Add("2G", 43);
            lowNoteValueDic.Add("2F", 41);
            lowNoteValueDic.Add("2E", 40);
            lowNoteValueDic.Add("2D", 38);
            lowNoteValueDic.Add("2C", 36);
            lowNoteValueDic.Add("1B", 35);
            lowNoteValueDic.Add("1A", 33);
            lowNoteValueDic.Add("1G", 31);
            lowNoteValueDic.Add("1F", 29);
            lowNoteValueDic.Add("1E", 28);
            lowNoteValueDic.Add("1D", 26);
            lowNoteValueDic.Add("1C", 24);
            lowNoteValueDic.Add("whole", -1);
            lowNoteValueDic.Add("half", -2);
            lowNoteValueDic.Add("quarter", -3);
            lowNoteValueDic.Add("eighth", -4);
            lowNoteValueDic.Add("16th", -5);
            lowNoteValueDic.Add("32nd", -6);
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

        public SequencerDemo.Note.Note GetNoteLocation(string defaultY,int staff)
        {
            int key = 0;
            if(staff == 2)
            {
                key = this.lowNoteValueDic[defaultY];
            }
            else
            {
                key = this.hightNoteValueDic[defaultY];
            }
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
