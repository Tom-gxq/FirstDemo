using Mongo.DAL;
using Mongo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Business
{
    public class MusicScoreBusiness
    {
        public static string AddMusicScore(SequencerDemo.Note.Score score)
        {
            Score insertObj = new Score
            {
                SID = Guid.NewGuid().ToString().ToLower(),
                Name = score.Name,
                Author = score.Author,
                FewShot = score.FewShot,
                OneShot = score.OneShot,
                OneShotNote = (int)score.OneShotNote,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                LastTime = DateTime.Now,
                MeasureList = GetMeasureByEntity(score.MeasureList)
            };

            return new MusicScore().AddMusicScore(insertObj);
        }

        private static List<Measure> GetMeasureByEntity(List<SequencerDemo.Note.Measure>list)
        {
            if (list != null)
            {
                return null;
            }
            List<Measure> mlist = new List<Measure>();
            foreach (var item in list )
            {
                Measure measure = new Measure()
                {
                    BarLine = (int)item.BarLine,
                    Beats = item.Beats,
                    BeatType = item.Beat_Type,
                    SID = Guid.NewGuid().ToString().ToLower(),
                    BlockList = GetNoteBlockByEntity(item.Blocks),
                    LowBlocks = GetNoteBlockByEntity(item.LowBlocks)
                };
                mlist.Add(measure);
            }
            return mlist;
        }

        private static List<NoteBlock> GetNoteBlockByEntity(List<SequencerDemo.Note.NoteBlock> list)
        {
            if (list != null)
            {
                return null;
            }
            List<NoteBlock> blist = new List<NoteBlock>();
            foreach (var item in list)
            {
                NoteBlock block = new NoteBlock()
                {
                    NBID = Guid.NewGuid().ToString().ToLower(),
                    Notes = GetNoteGroupByEntity(item.Notes)
                };
                blist.Add(block);
            }
            return blist;
        }

        private static List<NoteGroup> GetNoteGroupByEntity(List<SequencerDemo.Note.NoteGroup> list)
        {
            if (list != null)
            {
                return null;
            }
            List<NoteGroup> nglist = new List<NoteGroup>();
            foreach (var item in list)
            {
                NoteGroup ngroup = new NoteGroup()
                {
                    NGID = Guid.NewGuid().ToString().ToLower(),
                    NoteList = GetNoteByEntity(item.Notes)
                };
                nglist.Add(ngroup);
            }
            return nglist;
        }

        private static List<Note> GetNoteByEntity(List<SequencerDemo.Note.Note> list)
        {
            if (list != null)
            {
                return null;
            }
            List<Note> nlist = new List<Note>();
            foreach (var item in list)
            {
                Note note = new Note()
                {
                    NID = Guid.NewGuid().ToString().ToLower(),
                    Beams = item.Beams,
                    CrochetType = (int)item.CrochetType,
                    Data = item.Data,
                    DefaultX = item.DefaultX,
                    DefaultY = item.DefaultY,
                    Lift = (int)item.Lift,
                    Name = item.Name,
                    NoteType = (int)item.NoteType,
                    Octave = item.Octave,
                    Staff = item.Staff,
                    Voice = item.Voice,
                    NoteLocation = new NoteLocation()
                    {
                       LID = Guid.NewGuid().ToString().ToLower(),
                       Line = item.Location.line,
                       Offset = item.Location.offset,
                       SoundType = (int)item.Location.soundType
                    }
                };
                nlist.Add(note);
            }
            return nlist;
        }

    }
}
