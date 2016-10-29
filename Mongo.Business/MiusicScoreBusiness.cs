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
        #region 添加
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
                MeasureList = GetMeasureByEntity(score.MeasureList),
                
            };

            return new MusicScore().AddMusicScore(insertObj);
        }

        private static List<Measure> GetMeasureByEntity(List<SequencerDemo.Note.Measure>list)
        {
            if (list == null)
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
                    MID = Guid.NewGuid().ToString().ToLower(),
                    BlockList = GetNoteBlockByEntity(item.Blocks),
                    LowBlocks = GetNoteBlockByEntity(item.LowBlocks)
                };
                mlist.Add(measure);
            }
            return mlist;
        }

        private static List<NoteBlock> GetNoteBlockByEntity(List<SequencerDemo.Note.NoteBlock> list)
        {
            if (list == null)
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
            if (list == null)
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
            if (list == null)
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
        #endregion

        #region 查询
        public static SequencerDemo.Note.Score GetMusicScoreByID(string sid)
        {
            var mongoScore = new MusicScore().GetMusicScoreByID(sid);
            var score = new SequencerDemo.Note.Score()
            {
                SID = mongoScore.SID,
                Name = mongoScore.Name,
                Author = mongoScore.Author,
                FewShot = mongoScore.FewShot,
                OneShot = mongoScore.OneShot,
                OneShotNote = (SequencerDemo.Note.NoteType)mongoScore.OneShotNote,
                CreateTime = mongoScore.CreateTime,
                UpdateTime = mongoScore.UpdateTime,
                LastTime = mongoScore.LastTime,
                MeasureList = GetMeasureByMongoEntity(mongoScore.MeasureList)
            };
            return score;
        }
        public static List<SequencerDemo.Note.Score> GetMusicScores(int pageIndex, int pageSize)
        {
            var mongoScoreList = new MusicScore().GetMusicScores(pageIndex, pageSize);
            List<SequencerDemo.Note.Score> slist = new List<SequencerDemo.Note.Score>();
            foreach (var item in mongoScoreList)
            {
                var score = new SequencerDemo.Note.Score()
                {
                    SID = item.SID,
                    Name = item.Name
                };
                slist.Add(score);
            }
            return slist;
        }

        public static int GetMusicScoreCount()
        {
            return new MusicScore().GetMusicScoreCount();
        }
        private static List<SequencerDemo.Note.Measure> GetMeasureByMongoEntity(List<Entity.Measure> list)
        {
            if (list == null)
            {
                return null;
            }
            List<SequencerDemo.Note.Measure> mlist = new List<SequencerDemo.Note.Measure>();
            foreach (var item in list)
            {
                var measure = new SequencerDemo.Note.Measure()
                {
                    BarLine = (SequencerDemo.Note.BarLineType)item.BarLine,
                    Beats = item.Beats,
                    Beat_Type = item.BeatType,
                    MID = item.MID,
                    Blocks = GetNoteBlockByMongoEntity(item.BlockList),
                    LowBlocks = GetNoteBlockByMongoEntity(item.LowBlocks)
                };
                mlist.Add(measure);
            }
            return mlist;
        }

        private static List<SequencerDemo.Note.NoteBlock> GetNoteBlockByMongoEntity(List<NoteBlock> list)
        {
            if (list == null)
            {
                return null;
            }
            List<SequencerDemo.Note.NoteBlock> blist = new List<SequencerDemo.Note.NoteBlock>();
            foreach (var item in list)
            {
                SequencerDemo.Note.NoteBlock block = new SequencerDemo.Note.NoteBlock()
                {
                    NBID = item.NBID,
                    Notes = GetNoteGroupByMongoEntity(item.Notes)
                };
                blist.Add(block);
            }
            return blist;
        }

        private static List<SequencerDemo.Note.NoteGroup> GetNoteGroupByMongoEntity(List<NoteGroup> list)
        {
            if (list == null)
            {
                return null;
            }
            List<SequencerDemo.Note.NoteGroup> nglist = new List<SequencerDemo.Note.NoteGroup>();
            foreach (var item in list)
            {
                SequencerDemo.Note.NoteGroup ngroup = new SequencerDemo.Note.NoteGroup()
                {
                    NGID = item.NGID,
                    Notes = GetNoteByMongoEntity(item.NoteList)
                };
                nglist.Add(ngroup);
            }
            return nglist;
        }

        private static List<SequencerDemo.Note.Note> GetNoteByMongoEntity(List<Note> list)
        {
            if (list == null)
            {
                return null;
            }
            List<SequencerDemo.Note.Note> nlist = new List<SequencerDemo.Note.Note>();
            foreach (var item in list)
            {
                SequencerDemo.Note.Note note = new SequencerDemo.Note.Note()
                {
                    NID = item.NID,
                    Beams = item.Beams,
                    CrochetType = (SequencerDemo.Note.CrochetType)item.CrochetType,
                    Data = item.Data,
                    DefaultX = item.DefaultX,
                    DefaultY = item.DefaultY,
                    Lift = (SequencerDemo.Note.NoteLift)item.Lift,
                    Name = item.Name,
                    NoteType = (SequencerDemo.Note.NoteType)item.NoteType,
                    Octave = item.Octave,
                    Staff = item.Staff,
                    Voice = item.Voice,
                    Location = new Sanford.Multimedia.Midi.Config.NoteLocation()
                    {
                        line = item.NoteLocation.Line,
                        offset = item.NoteLocation.Offset,
                        soundType = (Sanford.Multimedia.Midi.Config.SoundDepart)item.NoteLocation.SoundType
                    }
                };
                nlist.Add(note);
            }
            return nlist;
        }
        #endregion

    }
}
