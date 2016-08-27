﻿using Sanford.Multimedia.Midi.Config;
using SequencerDemo.Note;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.xmlFile;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private SequencerDemo.Note.Score score = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.isRedraw = false;
                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
                string fileName = openFileDialog1.FileName;

                try
                {
                    this.score = new SequencerDemo.Note.Score();
                    XmlReader fileReader = new XmlReader(fileName,this.score);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)//重写 
        {
            if (this.isRedraw)
            {
                locationX = 0;//音符x坐标基准点
                locationY = 0;//音符y坐标基准点
                noteCount = 0;//每行音符数
                barCount = 0;//小节数
                DrawScore();
            }
            else
            {
                DrawNoteLine();
            }
        }

        private void DrawNoteLine()
        {
            Graphics g = pictureBox1.CreateGraphics();

            using (Pen myPen = new Pen(Color.Black, 2))
            {
                int len = (ClientRectangle.Height - 5) / TOTAL_LINE * LINE_HEIGHT_PER;
                int tmp = 6, start = 6;//高音线前空6条线
                for (int i = 1; i <= len; i++)
                {
                    start = tmp;
                    //高音线
                    int alllen = (tmp + 5);
                    for (int y = tmp; y < alllen; y++, tmp++)
                    {
                        g.DrawLine(myPen, new Point(0, y * LINE_HEIGHT_PER), new Point(ClientRectangle.Width, y * LINE_HEIGHT_PER));
                    }

                    tmp = alllen + 4;//高低音中间空4条
                    //低音线
                    alllen = (tmp + 5);
                    for (int y = tmp; y < alllen; y++, tmp++)
                    {
                        g.DrawLine(myPen, new Point(0, y * LINE_HEIGHT_PER), new Point(ClientRectangle.Width, y * LINE_HEIGHT_PER));
                    }

                    g.DrawLine(myPen, new Point(0, start * LINE_HEIGHT_PER), new Point(0, (tmp - 1) * LINE_HEIGHT_PER));
                    g.DrawLine(myPen, new Point(ClientRectangle.Width - 10, start * LINE_HEIGHT_PER), new Point(ClientRectangle.Width - 10, (tmp - 1) * LINE_HEIGHT_PER));
                    tmp += 6;
                }
            }
        }
        #region 画音符
        private int locationX = 0;//音符x坐标基准点
        private int locationY = 0;//音符y坐标基准点
        private int noteCount = 0;//每行音符数
        private int barCount = 0;//小节数
        private const int TOTAL_LINE = 20;//五线谱总共有多少条线
        private const int MESEAR_HEIGHT = 11;//五线谱总共有多少条线
        private const int LINE_HEIGHT_PER = 10;//五线谱每条线的高度
        private const int NOTE_VERTICAL_SPACING = 20;//每个音符的行间距
        private const int NOTE_TAIL_HEIGHT = LINE_HEIGHT_PER * 2 + LINE_HEIGHT_PER / 2;//音符尾巴高度
        private bool isRedraw = false;

        private void DrawNote(SequencerDemo.Note.NoteGroup group)
        {
            noteCount++;
            if (group == null)
            {
                return;
            }
            int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;
            foreach (var note in group.Notes)
            {
                int pointY = note.Location.line - MESEAR_HEIGHT;

                if (pointY != 0)
                {
                    pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset - 4;
                }                

                Graphics g = pictureBox1.CreateGraphics();
                using (Pen myPen = new Pen(Color.Red, 2))
                {
                    //画符头
                    switch (note.NoteType)
                    {
                        case NoteType.Whole://全音符
                            g.DrawEllipse(myPen, pointX, pointY, 6, 7);
                            noteCount += 3;
                            break;
                        case NoteType.Minims://二分音符
                            g.DrawEllipse(myPen, pointX, pointY, 6, 6);
                            noteCount++;
                            break;
                        case NoteType.CrotchetsC://四分音符
                        case NoteType.Quavers://八分音符
                        case NoteType.Demiquaver://十六分音符
                        case NoteType.Demisemiquaver://三十二分音符
                            g.DrawEllipse(myPen, pointX, pointY, 4, 5);
                            g.FillEllipse(new SolidBrush(Color.Red), pointX, pointY, 4, 5);
                            break;
                    }

                    if ((note.Location.line == 0) && (note.Location.offset == 0))
                    {
                        //画基准线
                        Point p1 = new Point(pointX - 4, pointY + 4);
                        Point p2 = new Point(pointX + 10, pointY + 4);
                        g.DrawLine(myPen, p1, p2);
                    }

                    //画符杆
                    if (note.NoteType != NoteType.Whole)
                    {
                        Point p1 = new Point(pointX + 4, pointY);
                        Point p2 = new Point(pointX + 4, pointY - NOTE_TAIL_HEIGHT);
                        if (note.NoteType == NoteType.Minims)
                        {
                            p1 = new Point(pointX + 6, pointY + 3);
                            p2 = new Point(pointX + 6, pointY - NOTE_TAIL_HEIGHT);
                        }

                        if (note.CrochetType == CrochetType.Down)
                        {
                            if (note.NoteType == NoteType.Minims)
                            {
                                p1 = new Point(pointX, pointY - 3);
                                p2 = new Point(pointX, pointY + NOTE_TAIL_HEIGHT);
                            }
                            else
                            {
                                p1 = new Point(pointX, pointY);
                                p2 = new Point(pointX, pointY + NOTE_TAIL_HEIGHT);
                            }
                        }
                        g.DrawLine(myPen, p1, p2);
                    }

                    //画升降号
                    if (note.Lift == NoteLift.Flat)
                    {
                        //画降号
                        Font font = new Font("宋体", 11);
                        SolidBrush mysbrush1 = new SolidBrush(Color.Blue);
                        g.DrawString("b", font, mysbrush1, pointX - 10, pointY - 5);
                    }
                    else if (note.Lift == NoteLift.Sharp)
                    {
                        //画升号
                        Font font = new Font("宋体", 11);
                        SolidBrush mysbrush1 = new SolidBrush(Color.Blue);
                        g.DrawString("#", font, mysbrush1, pointX - 10, pointY - 5);
                    }
                    else if (note.Lift == NoteLift.Natural)
                    {
                        //画还原号
                        Font font = new Font("宋体", 13);
                        SolidBrush mysbrush1 = new SolidBrush(Color.Blue);
                        g.DrawString("♮", font, mysbrush1, pointX - 16, pointY - 5);
                    }


                }
            }
        }

        /// <summary>
        /// 画终止符
        /// </summary>
        /// <param name="note"></param>
        private void DrawStopNote(SequencerDemo.Note.NoteGroup group)
        {
            if (group == null)
            {
                return;
            }
            switch (group.NoteType)
            {
                case NoteType.AllStop://全停止符
                    {
                        noteCount = 2;

                        int pointY = group.Notes[0].Location.line - TOTAL_LINE;

                        if (pointY != 0)
                        {
                            pointY = (-pointY + locationY) * LINE_HEIGHT_PER + group.Notes[0].Location.offset;
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
                    }
                    break;
                case NoteType.MinimsStop://二分停止符
                    {
                        noteCount++;
                        int pointY = group.Notes[0].Location.line - TOTAL_LINE;

                        if (pointY != 0)
                        {
                            pointY = (-pointY + locationY) * LINE_HEIGHT_PER + group.Notes[0].Location.offset;
                        }
                        int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

                        Graphics g = pictureBox1.CreateGraphics();
                        using (Pen myPen = new Pen(Color.Red, 2))
                        {
                            Point p1 = new Point(pointX, pointY);
                            Point p2 = new Point(pointX + 10, pointY);
                            g.DrawLine(myPen, p1, p2);
                        }
                        noteCount++;
                    }
                    break;
                case NoteType.CrotchetsCStop://四分停止符
                    {
                        noteCount++;
                        int pointY = 4 - TOTAL_LINE;

                        if (pointY != 0)
                        {
                            pointY = (-pointY + locationY) * LINE_HEIGHT_PER;
                        }
                        int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;


                        Graphics g = pictureBox1.CreateGraphics();
                        using (Pen myPen = new Pen(Color.Red, 2))
                        {
                            Point p1 = new Point(pointX, pointY);
                            Point p2 = new Point(pointX + 10, pointY + 3);
                            g.DrawLine(myPen, p1, p2);

                            RectangleF oval = new RectangleF((float)pointX, pointY + 3, 10, 10);
                            g.DrawArc(myPen, oval, 180, 150);


                            pointY = 3 - TOTAL_LINE;
                            if (pointY != 0)
                            {
                                pointY = (-pointY + locationY) * LINE_HEIGHT_PER;
                            }
                            pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

                            oval = new RectangleF((float)pointX - 5, pointY, 10, 10);
                            g.DrawArc(myPen, oval, -90, 300);

                            p1 = new Point(pointX - 5, pointY + 5);
                            p2 = new Point(pointX + 5, pointY + LINE_HEIGHT_PER);
                            g.DrawLine(myPen, p1, p2);
                        }
                    }
                    break;
                case NoteType.QuaversStop://八分停止符
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
                            g.DrawEllipse(myPen, pointX, pointY - 3, 4, 5);

                            RectangleF oval = new RectangleF((float)pointX + 5, pointY, 3, 5);
                            g.DrawArc(myPen, oval, 180, -270);

                            Point p1 = new Point(pointX + 8, pointY - 3);
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
        private Point GetBarEndPoint(SequencerDemo.Note.NoteGroup note,int count)
        {
            if (note == null)
            {
                return new Point();
            }
            int pointY = note.Location.line - MESEAR_HEIGHT;

            if (pointY != 0)
            {
                pointY = (-pointY + locationY) * LINE_HEIGHT_PER + note.Location.offset - 4;
            }
            int pointX = locationX + count * NOTE_VERTICAL_SPACING;

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
            var count = this.noteCount - block.Notes.Count()+1;            
            Point p1 = GetBarEndPoint(headNote, count);
            Point p2 = GetBarEndPoint(tailNote, this.noteCount);

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
                    case NoteType.Demisemiquaver:
                        g.DrawLine(myPen, p1, p2);
                        Point p7 = new Point();
                        Point p8 = new Point();
                        Point p9 = new Point();
                        Point p10 = new Point();
                        if (headNote.CrochetType == CrochetType.Down)
                        {
                            p7 = new Point(p1.X, p1.Y - 5);
                            p8 = new Point(p2.X, p2.Y - 5);

                            p9 = new Point(p1.X, p1.Y - 10);
                            p10 = new Point(p2.X, p2.Y - 10);
                        }
                        else
                        {
                            p7 = new Point(p1.X, p1.Y + 5);
                            p8 = new Point(p2.X, p2.Y + 5);

                            p9 = new Point(p1.X, p1.Y + 10);
                            p10 = new Point(p2.X, p2.Y + 10);
                        }
                        g.DrawLine(myPen, p7, p8);
                        g.DrawLine(myPen, p9, p10);
                        break;
                }
            }
        }

        /// <summary>
        /// 画音符的符尾
        /// </summary>
        /// <param name="block"></param>
        private void DrawSymbolNote(SequencerDemo.Note.NoteGroup group)
        {
            if (group == null)
            {
                return;
            }
            int pointY = group.Location.line - MESEAR_HEIGHT;

            if (pointY != 0)
            {
                pointY = (-pointY + locationY) * LINE_HEIGHT_PER + group.Location.offset - 4;
            }
            int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

            //计算符杆的终点位置
            Point p = new Point(pointX + 4, pointY - NOTE_TAIL_HEIGHT);
            if (group.CrochetType == CrochetType.Down)
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
                switch (group.NoteType)
                {
                    //画一条符尾
                    case NoteType.Quavers:
                        p1 = new Point(p.X + 8, p.Y);
                        g.DrawLine(myPen, p, p1);
                        break;
                    //画两条符尾
                    case NoteType.Demiquaver:
                        p1 = new Point(p.X + 8, p.Y);
                        g.DrawLine(myPen, p, p1);
                        p2 = new Point(p.X, p.Y + 4);
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
        private void DrawBar(SequencerDemo.Note.NoteGroup group, Measure bar,int i)
        {
            if (group == null)
            {
                return;
            }
            long barWidth = bar.NoteNum * NOTE_VERTICAL_SPACING;
            int overWidth = this.pictureBox1.Width - (noteCount + this.barCount + 1) * NOTE_VERTICAL_SPACING;
            if ((overWidth < 0) || (overWidth / barWidth <= 0))
            {
                locationY += TOTAL_LINE;
                locationX = 0;
                noteCount = 0;
            }
            else
            {
                int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

                //画小节线
                Graphics g = pictureBox1.CreateGraphics();
                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    Point pp1 = new Point(pointX + NOTE_VERTICAL_SPACING, (locationY+6 ) * LINE_HEIGHT_PER);
                    Point pp2 = new Point(pointX + NOTE_VERTICAL_SPACING, (locationY+ 15) * LINE_HEIGHT_PER + 4 * LINE_HEIGHT_PER);
                    g.DrawLine(pen, pp1, pp2);
                }

                locationX += NOTE_VERTICAL_SPACING;
            }

        }

        private void DrawBarLine(Measure bar)
        {
            if(bar.BarLine > BarLineType.None)
            {
                var lastBlock = bar.Blocks[bar.Blocks.Count - 1];
                var lastNote = lastBlock.Notes[lastBlock.Notes.Count - 1];
                int pointX = locationX + noteCount * NOTE_VERTICAL_SPACING;

                Graphics g = pictureBox1.CreateGraphics();
                Point pp1 = new Point(pointX + NOTE_VERTICAL_SPACING, (locationY+6 )  * LINE_HEIGHT_PER);
                Point pp2 = new Point(pointX + NOTE_VERTICAL_SPACING, (locationY + 15) * LINE_HEIGHT_PER + 5 * LINE_HEIGHT_PER);

                
                switch (bar.BarLine)
                {
                    case BarLineType.Regular:
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            g.DrawLine(pen, pp1, pp2);                            
                        }
                        break;
                    case BarLineType.Dotted:
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.Dashed:
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.Heavy:
                        using (Pen pen = new Pen(Color.Black, 5))
                        {                            
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.DobuleLight:
                        using (Pen pen = new Pen(Color.Gray, 2))
                        {
                            g.DrawLine(pen, pp1, pp2);
                            pp1.X += 5;
                            pp2.X += 5;
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.LightHeavy:
                        using (Pen pen = new Pen(Color.Gray, 2))
                        {
                            g.DrawLine(pen, pp1, pp2);
                        }
                        pp1.X += 5;
                        pp2.X += 5;
                        using (Pen pen = new Pen(Color.Black, 5))
                        {
                            g.DrawLine(pen, pp1, pp2);                            
                        }
                        break;
                    case BarLineType.HeavyLight:
                        using (Pen pen = new Pen(Color.Black, 5))
                        {
                            g.DrawLine(pen, pp1, pp2);
                        }
                        pp1.X += 5;
                        pp2.X += 5;
                        using (Pen pen = new Pen(Color.Gray, 2))
                        {
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.DobuleHeavy:
                        using (Pen pen = new Pen(Color.Gray, 5))
                        {
                            g.DrawLine(pen, pp1, pp2);
                            pp1.X += 5;
                            pp2.X += 5;
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.Tick:
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            pp2.Y = pp1.Y + 10;
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                    case BarLineType.Short:
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            pp1.Y = pp1.Y + 30;
                            pp2.Y = pp2.Y -20;
                            g.DrawLine(pen, pp1, pp2);
                        }
                        break;
                }
                locationX += NOTE_VERTICAL_SPACING;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.isRedraw = true;
            DrawScore();
        }

        private void DrawScore()
        {
            this.labName.Text = this.score.Name;
            this.labAuthor.Text = this.score.Author;
            var list = this.score.MeasureList;

            if (list != null)
            {
                DrawNoteLine();
                SequencerDemo.Note.NoteGroup lastNote = null;
                for (int i = 0; i < list.Count; i++)
                {
                    if (i >= 0)
                    {
                        DrawBar(lastNote, list[i], i);
                    }
                    if ((list[i] != null) && (list[i].Blocks != null))
                    {
                        for (int j = 0; j < list[i].Blocks.Count; j++)
                        {
                            var block = list[i].Blocks[j];
                            if ((block != null) && (block.Notes != null))
                            {
                                foreach (var group in block.Notes)
                                {
                                    lastNote = group;
                                    if ((group.NoteType >= NoteType.AllStop) && (group.NoteType <= NoteType.QuaversStop))
                                    {
                                        DrawStopNote(group);
                                    }
                                    else
                                    {
                                        DrawNote(group);
                                    }
                                }
                                if ((block.Notes.Count > 1) || (lastNote.NoteType >= NoteType.AllStop))
                                {
                                    //多个音符画符杠
                                    DrawSymbolBar(block);
                                }
                                else
                                {
                                    //单个音符画符尾
                                    DrawSymbolNote(lastNote);
                                }
                            }

                        }

                    }
                    DrawBarLine(list[i]);
                    this.barCount++;
                }
            }
        }
    }
}