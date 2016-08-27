using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public class Score
    {
        private string name;
        private string author;//作者
        private Stack<NoteBar> barList =new Stack<NoteBar>();//高音部小节
        private Stack<NoteBar> lowBarList = new Stack<NoteBar>();//低音部小节
        private int fewShot;//一小节有几拍
        private int oneShot;//分母
        private NoteType oneShotNote;//以什么样的音符为一拍
        private int bitTime;//一拍的时间（单位tick）
        private int headLen;//midi头长

        private List<Measure> measureList = new List<Measure>();//高音部小节
        private List<Measure> lowMeasureList = new List<Measure>();//低音部小节


        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }
        public int FewShot
        {
            get
            {
                return this.fewShot;
            }
            set
            {
                this.fewShot = value;
            }
        }

        public int OneShot
        {
            get
            {
                return this.oneShot;
            }
            set
            {
                this.oneShot = value;
            }
        }

        public NoteType OneShotNote
        {
            get
            {
                return this.oneShotNote;
            }
            set
            {
                this.oneShotNote = value;
            }
        }

        public int BitTime
        {
            get
            {
                return this.bitTime;
            }
            set
            {
                this.bitTime = value;
            }
        }
        
        public int HeadLen
        {
            get
            {
                return this.headLen;
            }
            set
            {
                this.headLen = value;
            }
        }
        public List<NoteBar> NoteBars
        {
            get
            {
                return new List<NoteBar>(this.barList.ToArray());
            }
            set
            {
                if (value != null)
                {
                    value.ForEach(x => this.barList.Push(x));
                }
            }
        }

        public NoteBar GetLastBar(int data)
        {
            bool ret = NoteScoreTable.Instance.IsLowNote(data);
            NoteBar lastBar = null;
            if (ret)
            {
                if (this.lowBarList.Count > 0)
                {
                    lastBar = this.lowBarList.Pop();
                    this.lowBarList.Push(lastBar);
                    return lastBar;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (this.barList.Count > 0)
                {
                    lastBar = this.barList.Pop();
                    this.barList.Push(lastBar);
                    return lastBar;
                }
                else
                {
                    return null;
                }
            }
        }
        public void AddNoteBars(NoteBar noteBar)
        {
            this.barList.Push(noteBar);
        }
        
        public void AddNote(int data1, ChannelCommand command,int ticks)
        {
            bool ret = NoteScoreTable.Instance.IsLowNote(data1);
            if (ret)
            {
                AddNoteToLowBar(data1, command, ticks);
            }
            else
            {
                AddNoteToHightBar(data1, command, ticks);
            }
        }

        public void AddNoteToHightBar(int data1, ChannelCommand command, int ticks)
        {
            if (command == ChannelCommand.NoteOn)
            {
                NoteBar bar = null;
                if (this.barList.Count > 0)
                {
                    bar = this.barList.Pop();
                    int paiCnt = bar.BarTicks % this.BitTime;
                    if (((bar.BarTicks / this.BitTime) >= this.fewShot)
                        || ((paiCnt > 0) && (bar.BarTicks > this.bitTime) && ((this.BitTime % paiCnt > 0))))
                    {
                        this.barList.Push(bar);
                        bar = new NoteBar(this.bitTime,this);
                        if (this.lowBarList.Count > 0)
                        {
                            AddStopToLowBar();
                        }
                    }
                }
                else
                {
                    bar = new NoteBar(this.bitTime,this);
                    if (this.lowBarList.Count > 0)
                    {
                        AddStopToLowBar();
                    }
                }

                //开始播放命令前面有时间的话，添加结束符
                if (ticks > 0)
                {
                    int data2 = GetStopNoteValue(ticks);
                    //如果是第一个小节前有停止符，则添加到当前的小节中
                    if (this.barList.Count <= 0)
                    {
                        bar.AddNote(data2, command, ticks);
                        //如果该停止符是全停止符，则再生成个小节添加音符
                        if (data2 == (int)StopNoteVal.AllStop)
                        {
                            this.barList.Push(bar);
                            bar = new NoteBar(this.bitTime,this);
                            if (this.lowBarList.Count > 0)
                            {
                                AddStopToLowBar();
                            }
                        }
                    }
                    //如果小节中已经有音符且小节未满，停止符添加到当前小节中
                    else if(bar.BarTicks > 0)
                    {
                        bar.AddNote(data2, command, ticks);
                        //添加停止符后，小节满了，则再创建一个小节，添加音符
                        if (bar.BarTicks >= this.fewShot * this.bitTime)
                        {
                            this.barList.Push(bar);
                            bar = new NoteBar(this.bitTime, this);
                            if (this.lowBarList.Count > 0)
                            {
                                AddStopToLowBar();
                            }
                        }
                    }
                    //如果是当前小节的第一个音符，且前面还有其他小节，则添加到前面的小节中
                    else
                    {
                        var preBar = this.barList.Pop();
                        preBar.AddNote(data2, command, ticks);
                        this.barList.Push(preBar);
                    }
                   
                    //添加结束符后，小节时间满了的话，添加新的小节
                    if(bar.BarTicks == (this.fewShot*this.bitTime))
                    {
                        this.barList.Push(bar);
                        bar = new NoteBar(this.bitTime,this);
                        ticks = 0;
                    }
                }
                bar.AddNote(data1, command, ticks);
                this.barList.Push(bar);
            }
            else
            {
                if (this.barList.Count > 0)
                {
                    var bar = this.barList.Pop();
                    bar.AddNote(data1, command, ticks);
                    this.barList.Push(bar);
                }
            }
        }

        private NoteBar preHightBar = null;
        public void AddNoteToLowBar(int data1, ChannelCommand command, int ticks)
        {
            if (command == ChannelCommand.NoteOn)
            {
                NoteBar bar = null;
                NoteBlock lastBlock = null;
                if (this.lowBarList.Count <= 0)
                {
                    for (int i = 0; i < (this.barList.Count-1); i++)
                    {
                        AddStopToLowBar();
                    }
                }
                if(this.barList.Count > 0)
                {
                    preHightBar = this.barList.Pop();
                    if (preHightBar.Notes.Count > 0)
                    {
                        lastBlock = preHightBar.Notes[preHightBar.Notes.Count - 1];
                    }                    
                    this.barList.Push(preHightBar);
                }

                if(lastBlock  != null)
                {
                    //低音音符前如果没有音符的话，前面加停止符
                    var stopBar = new NoteBar(this.bitTime,this);
                    int data2 = GetStopNoteValue(lastBlock.BlockTicks);
                    stopBar.AddNote(data1, command, lastBlock.BlockTicks);
                    this.lowBarList.Push(stopBar);
                }
                if (this.lowBarList.Count > 0)
                {
                    bar = this.lowBarList.Pop();
                    int paiCnt = bar.BarTicks % this.BitTime;
                    if (((bar.BarTicks / this.BitTime) >= this.fewShot)//小节拍数已满
                        || ((paiCnt > 0) && (bar.BarTicks > this.bitTime) && ((this.BitTime % paiCnt > 0)))//小节中已有拍数，且小节已有时间大于一拍时间
                        )
                    {
                        this.lowBarList.Push(bar);
                        bar = new NoteBar(this.bitTime,this);
                    }
                }
                else
                {
                    bar = new NoteBar(this.bitTime,this);
                }


                bar.AddNote(data1, command, ticks);
                this.lowBarList.Push(bar);
            }
            else
            {
                if (this.lowBarList.Count > 0)
                {
                    var bar = this.lowBarList.Pop();
                    bar.AddNote(data1, command, ticks);
                    this.lowBarList.Push(bar);
                    preHightBar.AddNote(data1, command, ticks);
                }
            }
        }
        public void AddStopToLowBar()
        {
            NoteBar bar = new NoteBar(this.bitTime,this);
            bar.AddNote((int)StopNoteVal.AllStop, ChannelCommand.NoteOn, this.bitTime);
            this.lowBarList.Push(bar);
        }

        private int GetStopNoteValue(int ticks)
        {
            int data2 = 0;
            if (ticks / this.bitTime == this.fewShot)
            {
                data2 = (int)StopNoteVal.AllStop;
            }
            else if (ticks / this.bitTime == this.fewShot / 2)
            {
                data2 = (int)StopNoteVal.MinimsStop;
            }
            else if (ticks / this.bitTime == this.fewShot / 4)
            {
                data2 = (int)StopNoteVal.CrotchetsCStop;
            }
            else if (ticks / this.bitTime == this.fewShot / 8)
            {
                data2 = (int)StopNoteVal.QuaversStop;
            }
            return data2;
        }

        public void AddMeasure(Measure measure)
        {
            if(measure.LowBlocks.Count > 0)
            {
                Measure lowMeasure = new Measure();
                lowMeasure.Blocks = measure.LowBlocks;
                this.lowMeasureList.Add(lowMeasure);
            }

            this.measureList.Add(measure);
        }

        public List<Measure> MeasureList
        {
            get
            {
                return this.measureList;
            }
        }
    }
}
