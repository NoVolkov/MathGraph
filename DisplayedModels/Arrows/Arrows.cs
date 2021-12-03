using System;

namespace Arrows
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Shapes;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows;
    using System.Windows.Media;

    public class AdjustableArrowBezierCurve : ArrowBezierCurve
    {
        #region Fields

        /// <summary>
        /// 是否显示控制的依赖属性
        /// </summary>
        public static readonly DependencyProperty ShowControlProperty = DependencyProperty.Register(
            "ShowControl",
            typeof(bool),
            typeof(AdjustableArrowBezierCurve),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 控制点椭圆半径
        /// </summary>
        private const double EllipseRadius = 5;

        /// <summary>
        /// 连线画笔
        /// </summary>
        private readonly Pen linePen = new Pen(Brushes.Black, 1);

        /// <summary>
        /// 控制点画刷
        /// </summary>
        private readonly Brush ellipseBrush = Brushes.Black;

        /// <summary>
        /// 控制点椭圆画笔
        /// </summary>
        private readonly Pen ellipsePen = new Pen(Brushes.Black, 1);

        /// <summary>
        /// 是否按下控制点1
        /// </summary>
        private bool isPressedControlPoint1;

        /// <summary>
        /// 是否按下控制点2
        /// </summary>
        private bool isPressedControlPoint2;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 是否显示控制
        /// </summary>
        public bool ShowControl
        {
            get { return (bool)this.GetValue(ShowControlProperty); }
            set { this.SetValue(ShowControlProperty, value); }
        }

        #endregion Properties

        #region Overrides

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseDown"/> 附加事件在其路由中到达派生自此类的元素时，调用该方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseButtonEventArgs"/>。此事件数据报告有关按下的鼠标按钮和已处理状态的详细信息。
        /// </param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.ShowControl && (e.LeftButton == MouseButtonState.Pressed))
            {
                this.CaptureMouse();
                Point pt = e.GetPosition(this);
                Vector slide = pt - this.ControlPoint1;
                if (slide.Length < EllipseRadius)
                {
                    this.isPressedControlPoint1 = true;
                }

                slide = pt - this.ControlPoint2;
                if (slide.Length < EllipseRadius)
                {
                    this.isPressedControlPoint2 = true;
                }
            }
        }

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseUp"/> 路由事件在其路由中到达派生自此类的元素时，调用该方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseButtonEventArgs"/>。事件数据将报告已释放了鼠标按钮。
        /// </param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.ReleaseMouseCapture();
            this.isPressedControlPoint1 = false;
            this.isPressedControlPoint2 = false;
        }

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseMove"/> 附加事件在其路由中到达派生自此类的元素时，调用该方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseEventArgs"/>。
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.ShowControl && (e.LeftButton == MouseButtonState.Pressed))
            {
                var currentPt = e.GetPosition(this);
                if (this.isPressedControlPoint1)
                {
                    this.ControlPoint1 = currentPt;
                }

                if (this.isPressedControlPoint2)
                {
                    this.ControlPoint2 = currentPt;
                }
            }
        }

        /// <summary>
        /// 在派生类中重写时，会参与由布局系统控制的呈现操作。调用此方法时，不直接使用此元素的呈现指令，而是将其保留供布局和绘制在以后异步使用。
        /// </summary>
        /// <param name="drawingContext">特定元素的绘制指令。此上下文是为布局系统提供的。
        /// </param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.ShowControl)
            {
                drawingContext.DrawLine(this.linePen, this.StartPoint, this.ControlPoint1);
                drawingContext.DrawEllipse(this.ellipseBrush, this.ellipsePen, this.ControlPoint1, EllipseRadius, EllipseRadius);

                drawingContext.DrawLine(this.linePen, this.EndPoint, this.ControlPoint2);
                drawingContext.DrawEllipse(this.ellipseBrush, this.ellipsePen, this.ControlPoint2, EllipseRadius, EllipseRadius);
            }
        }

        #endregion Overrides
    }
    public class AdjustableArrowQuadraticBezier : ArrowQuadraticBezier
    {
        #region Fields

        /// <summary>
        /// 是否显示控制的依赖属性
        /// </summary>
        public static readonly DependencyProperty ShowControlProperty = DependencyProperty.Register(
            "ShowControl",
            typeof(bool),
            typeof(AdjustableArrowQuadraticBezier),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 控制点椭圆半径
        /// </summary>
        private const double EllipseRadius = 5;

        /// <summary>
        /// 连线画笔
        /// </summary>
        private readonly Pen linePen = new Pen(Brushes.Black, 1);

        /// <summary>
        /// 控制点椭圆画刷
        /// </summary>
        private readonly Brush ellipseBrush = Brushes.Black;

        /// <summary>
        /// 控制点椭圆画笔
        /// </summary>
        private readonly Pen ellipsePen = new Pen(Brushes.Black, 1);

        /// <summary>
        /// 是否按下控制点
        /// </summary>
        private bool isPressedControlPoint;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 是否显示控制
        /// </summary>
        public bool ShowControl
        {
            get { return (bool)this.GetValue(ShowControlProperty); }
            set { this.SetValue(ShowControlProperty, value); }
        }

        #endregion Properties

        #region Overrides

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseDown"/> 附加事件在其路由中到达派生自此类的元素时，调用该方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseButtonEventArgs"/>。此事件数据报告有关按下的鼠标按钮和已处理状态的详细信息。
        /// </param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.ShowControl && (e.LeftButton == MouseButtonState.Pressed))
            {
                this.CaptureMouse();
                Point pt = e.GetPosition(this);
                Vector slide = pt - this.ControlPoint;

                // 在控制点的圆圈之内
                if (slide.Length < EllipseRadius)
                {
                    this.isPressedControlPoint = true;
                }
            }
        }

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseUp"/> 路由事件在其路由中到达派生自此类的元素时，调用该方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseButtonEventArgs"/>。事件数据将报告已释放了鼠标按钮。
        /// </param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.ReleaseMouseCapture();
            this.isPressedControlPoint = false;
        }

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseMove"/> 附加事件在其路由中到达派生自此类的元素时，调用该方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseEventArgs"/>。
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.ShowControl && (e.LeftButton == MouseButtonState.Pressed) && this.isPressedControlPoint)
            {
                // 更新控制点
                this.ControlPoint = e.GetPosition(this);
            }
        }

        /// <summary>
        /// 在派生类中重写时，会参与由布局系统控制的呈现操作。调用此方法时，不直接使用此元素的呈现指令，而是将其保留供布局和绘制在以后异步使用。
        /// </summary>
        /// <param name="drawingContext">特定元素的绘制指令。此上下文是为布局系统提供的。
        /// </param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.ShowControl)
            {
                drawingContext.DrawLine(this.linePen, this.StartPoint, this.ControlPoint);
                drawingContext.DrawEllipse(this.ellipseBrush, this.ellipsePen, this.ControlPoint, EllipseRadius, EllipseRadius);
            }
        }

        #endregion Overrides
    }
    public abstract class ArrowBase : Shape
    {
        #region Fields

        #region DependencyProperty

        /// <summary>
        /// 箭头两边夹角的依赖属性
        /// </summary>
        public static readonly DependencyProperty ArrowAngleProperty = DependencyProperty.Register(
            "ArrowAngle",
            typeof(double),
            typeof(ArrowBase),
            new FrameworkPropertyMetadata(45.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 箭头长度的依赖属性
        /// </summary>
        public static readonly DependencyProperty ArrowLengthProperty = DependencyProperty.Register(
            "ArrowLength",
            typeof(double),
            typeof(ArrowBase),
            new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 箭头所在端的依赖属性
        /// </summary>
        public static readonly DependencyProperty ArrowEndsProperty = DependencyProperty.Register(
            "ArrowEnds",
            typeof(ArrowEnds),
            typeof(ArrowBase),
            new FrameworkPropertyMetadata(ArrowEnds.End, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 箭头是否闭合的依赖属性
        /// </summary>
        public static readonly DependencyProperty IsArrowClosedProperty = DependencyProperty.Register(
            "IsArrowClosed",
            typeof(bool),
            typeof(ArrowBase),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 开始点
        /// </summary>
        public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register(
            "StartPoint",
            typeof(Point),
            typeof(ArrowBase),
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion DependencyProperty

        /// <summary>
        /// 整个形状(包含箭头和具体形状)
        /// </summary>
        private readonly PathGeometry geometryWhole = new PathGeometry();

        /// <summary>
        /// 除去箭头外的具体形状
        /// </summary>
        private readonly PathFigure figureConcrete = new PathFigure();

        /// <summary>
        /// 开始处的箭头线段
        /// </summary>
        private readonly PathFigure figureStart = new PathFigure();

        /// <summary>
        /// 结束处的箭头线段
        /// </summary>
        private readonly PathFigure figureEnd = new PathFigure();

        #endregion Fields

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        protected ArrowBase()
        {
            var polyLineSegStart = new PolyLineSegment();
            this.figureStart.Segments.Add(polyLineSegStart);

            var polyLineSegEnd = new PolyLineSegment();
            this.figureEnd.Segments.Add(polyLineSegEnd);

            this.geometryWhole.Figures.Add(this.figureConcrete);
            this.geometryWhole.Figures.Add(this.figureStart);
            this.geometryWhole.Figures.Add(this.figureEnd);
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// 箭头两边夹角
        /// </summary>
        public double ArrowAngle
        {
            get { return (double)this.GetValue(ArrowAngleProperty); }
            set { this.SetValue(ArrowAngleProperty, value); }
        }

        /// <summary>
        /// 箭头两边的长度
        /// </summary>
        public double ArrowLength
        {
            get { return (double)this.GetValue(ArrowLengthProperty); }
            set { this.SetValue(ArrowLengthProperty, value); }
        }

        /// <summary>
        /// 箭头所在端
        /// </summary>
        public ArrowEnds ArrowEnds
        {
            get { return (ArrowEnds)this.GetValue(ArrowEndsProperty); }
            set { this.SetValue(ArrowEndsProperty, value); }
        }

        /// <summary>
        /// 箭头是否闭合
        /// </summary>
        public bool IsArrowClosed
        {
            get { return (bool)this.GetValue(IsArrowClosedProperty); }
            set { this.SetValue(IsArrowClosedProperty, value); }
        }

        /// <summary>
        /// 开始点
        /// </summary>
        public Point StartPoint
        {
            get { return (Point)this.GetValue(StartPointProperty); }
            set { this.SetValue(StartPointProperty, value); }
        }

        /// <summary>
        /// 定义形状
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                this.figureConcrete.StartPoint = this.StartPoint;

                // 清空具体形状,避免重复添加
                this.figureConcrete.Segments.Clear();
                var segements = this.FillFigure();
                if (segements != null)
                {
                    foreach (var segement in segements)
                    {
                        this.figureConcrete.Segments.Add(segement);
                    }
                }

                // 绘制开始处的箭头
                if ((this.ArrowEnds & ArrowEnds.Start) == ArrowEnds.Start)
                {
                    this.CalculateArrow(this.figureStart, this.GetStartArrowEndPoint(), this.StartPoint);
                }

                // 绘制结束处的箭头
                if ((this.ArrowEnds & ArrowEnds.End) == ArrowEnds.End)
                {
                    this.CalculateArrow(this.figureEnd, this.GetEndArrowStartPoint(), this.GetEndArrowEndPoint());
                }

                return this.geometryWhole;
            }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// 获取具体形状的各个组成部分
        /// </summary>
        /// <returns>PathSegment集合</returns>
        protected abstract PathSegmentCollection FillFigure();

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns>开始箭头处的结束点</returns>
        protected abstract Point GetStartArrowEndPoint();

        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns>结束箭头处的开始点</returns>
        protected abstract Point GetEndArrowStartPoint();

        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns>结束箭头处的结束点</returns>
        protected abstract Point GetEndArrowEndPoint();

        #endregion  Protected Methods

        #region Private Methods

        /// <summary>
        /// 计算两个点之间的有向箭头
        /// </summary>
        /// <param name="pathfig">箭头所在的形状</param>
        /// <param name="startPoint">开始点</param>
        /// <param name="endPoint">结束点</param>
        private void CalculateArrow(PathFigure pathfig, Point startPoint, Point endPoint)
        {
            var polyseg = pathfig.Segments[0] as PolyLineSegment;
            if (polyseg != null)
            {
                var matx = new Matrix();
                Vector vect = startPoint - endPoint;

                // 获取单位向量
                vect.Normalize();
                vect *= this.ArrowLength;

                // 旋转夹角的一半
                matx.Rotate(this.ArrowAngle / 2);

                // 计算上半段箭头的点
                pathfig.StartPoint = endPoint + (vect * matx);

                polyseg.Points.Clear();
                polyseg.Points.Add(endPoint);

                matx.Rotate(-this.ArrowAngle);

                // 计算下半段箭头的点
                polyseg.Points.Add(endPoint + (vect * matx));
            }

            pathfig.IsClosed = this.IsArrowClosed;
        }

        #endregion Private Methods
    }
    public class ArrowLine : ArrowBase
    {
        #region Fields

        /// <summary>
        /// 结束点
        /// </summary>
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(
            "EndPoint",
            typeof(Point),
            typeof(ArrowLine),
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 线段
        /// </summary>
        private readonly LineSegment lineSegment = new LineSegment();

        #endregion Fields

        #region Properties

        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)this.GetValue(EndPointProperty); }
            set { this.SetValue(EndPointProperty, value); }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// 填充Figure
        /// </summary>
        /// <returns>PathSegment集合</returns>
        protected override PathSegmentCollection FillFigure()
        {
            this.lineSegment.Point = this.EndPoint;
            return new PathSegmentCollection
            {
                this.lineSegment
            };
        }

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns>开始箭头处的结束点</returns>
        protected override Point GetStartArrowEndPoint()
        {
            return this.EndPoint;
        }

        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns>结束箭头处的开始点</returns>
        protected override Point GetEndArrowStartPoint()
        {
            return this.StartPoint;
        }

        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns>结束箭头处的结束点</returns>
        protected override Point GetEndArrowEndPoint()
        {
            return this.EndPoint;
        }

        #endregion  Protected Methods
    }
    [Flags]
    public enum ArrowEnds
    {
        /// <summary>
        /// 无箭头
        /// </summary>
        None = 0,

        /// <summary>
        /// 开始方向箭头
        /// </summary>
        Start = 1,

        /// <summary>
        /// 结束方向箭头
        /// </summary>
        End = 2,

        /// <summary>
        /// 两端箭头
        /// </summary>
        Both = 3
    }
    public class ArrowBezierCurve : ArrowBase
    {
        #region Fields

        #region DependencyProperty

        /// <summary>
        /// 控制点1
        /// </summary>
        public static readonly DependencyProperty ControlPoint1Property = DependencyProperty.Register(
            "ControlPoint1",
            typeof(Point),
            typeof(ArrowBezierCurve),
            new FrameworkPropertyMetadata(
                new Point(),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 控制点2
        /// </summary>
        public static readonly DependencyProperty ControlPoint2Property = DependencyProperty.Register(
            "ControlPoint2", typeof(Point), typeof(ArrowBezierCurve), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 结束点
        /// </summary>
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(
            "EndPoint", typeof(Point), typeof(ArrowBezierCurve), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion DependencyProperty

        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        private readonly BezierSegment bezierSegment = new BezierSegment();

        #endregion Fields

        #region Properties

        /// <summary>
        /// 控制点1
        /// </summary>
        public Point ControlPoint1
        {
            get { return (Point)this.GetValue(ControlPoint1Property); }
            set { this.SetValue(ControlPoint1Property, value); }
        }

        /// <summary>
        /// 控制点2
        /// </summary>
        public Point ControlPoint2
        {
            get { return (Point)this.GetValue(ControlPoint2Property); }
            set { this.SetValue(ControlPoint2Property, value); }
        }

        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)this.GetValue(EndPointProperty); }
            set { this.SetValue(EndPointProperty, value); }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// 填充Figure
        /// </summary>
        /// <returns>PathSegment集合</returns>
        protected override PathSegmentCollection FillFigure()
        {
            this.bezierSegment.Point1 = this.ControlPoint1;
            this.bezierSegment.Point2 = this.ControlPoint2;
            this.bezierSegment.Point3 = this.EndPoint;

            return new PathSegmentCollection
            {
                this.bezierSegment
            };
        }

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns>开始箭头处的结束点</returns>
        protected override Point GetStartArrowEndPoint()
        {
            return this.ControlPoint1;
        }

        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns>结束箭头处的开始点</returns>
        protected override Point GetEndArrowStartPoint()
        {
            return this.ControlPoint2;
        }

        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns>结束箭头处的结束点</returns>
        protected override Point GetEndArrowEndPoint()
        {
            return this.EndPoint;
        }

        #endregion  Protected Methods
    }
    public class ArrowQuadraticBezier : ArrowBase
    {
        #region Fields

        #region DependencyProperty

        /// <summary>
        /// 控制点1
        /// </summary>
        public static readonly DependencyProperty ControlPointProperty = DependencyProperty.Register(
            "ControlPoint",
            typeof(Point),
            typeof(ArrowQuadraticBezier),
            new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 结束点
        /// </summary>
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(
            "EndPoint",
            typeof(Point),
            typeof(ArrowQuadraticBezier),
            new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion DependencyProperty

        /// <summary>
        /// 二次贝塞尔曲线
        /// </summary>
        private readonly QuadraticBezierSegment quadraticBezierSegment = new QuadraticBezierSegment();

        #endregion Fields

        #region Properties

        /// <summary>
        /// 控制点1
        /// </summary>
        public Point ControlPoint
        {
            get { return (Point)this.GetValue(ControlPointProperty); }
            set { this.SetValue(ControlPointProperty, value); }
        }

        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)this.GetValue(EndPointProperty); }
            set { this.SetValue(EndPointProperty, value); }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// 填充Figure
        /// </summary>
        /// <returns>PathSegment集合</returns>
        protected override PathSegmentCollection FillFigure()
        {
            this.quadraticBezierSegment.Point1 = this.ControlPoint;
            this.quadraticBezierSegment.Point2 = this.EndPoint;

            return new PathSegmentCollection
            {
                this.quadraticBezierSegment
            };
        }

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns>开始箭头处的结束点</returns>
        protected override Point GetStartArrowEndPoint()
        {
            return this.ControlPoint;
        }

        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns>结束箭头处的开始点</returns>
        protected override Point GetEndArrowStartPoint()
        {
            return this.ControlPoint;
        }

        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns>结束箭头处的结束点</returns>
        protected override Point GetEndArrowEndPoint()
        {
            return this.EndPoint;
        }

        #endregion  Protected Methods
    }
    public class ArrowLineWithText : ArrowLine
    {
        #region DependencyProperty

        /// <summary>
        /// 文本的依赖属性
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 文本对齐的依赖属性
        /// </summary>
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
            "TextAlignment",
            typeof(TextAlignment),
            typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 文本朝上的依赖属性
        /// </summary>
        public static readonly DependencyProperty IsTextUpProperty = DependencyProperty.Register(
            "IsTextUp",
            typeof(bool),
            typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 是否显示文本的依赖属性
        /// </summary>
        public static readonly DependencyProperty ShowTextProperty = DependencyProperty.Register(
            "ShowText",
            typeof(bool),
            typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion DependencyProperty

        #region Properties

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)this.GetValue(TextAlignmentProperty); }
            set { this.SetValue(TextAlignmentProperty, value); }
        }

        /// <summary>
        /// 文本是否朝上
        /// </summary>
        public bool IsTextUp
        {
            get { return (bool)this.GetValue(IsTextUpProperty); }
            set { this.SetValue(IsTextUpProperty, value); }
        }

        /// <summary>
        /// 是否显示文本
        /// </summary>
        public bool ShowText
        {
            get { return (bool)this.GetValue(ShowTextProperty); }
            set { this.SetValue(ShowTextProperty, value); }
        }

        #endregion Properties

        #region Overrides

        /// <summary>
        /// 重载渲染事件
        /// </summary>
        /// <param name="drawingContext">绘图上下文</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.ShowText && (this.Text != null))
            {
                var txt = this.Text.Trim();
                var startPoint = this.StartPoint;
                if (!string.IsNullOrEmpty(txt))
                {
                    var vec = this.EndPoint - this.StartPoint;
                    var angle = this.GetAngle(this.StartPoint, this.EndPoint);

                    // 使用旋转变换,使其与线平行
                    var transform = new RotateTransform(angle)
                    {
                        CenterX = this.StartPoint.X,
                        CenterY = this.StartPoint.Y
                    };
                    drawingContext.PushTransform(transform);

                    var defaultTypeface = new Typeface(
                        SystemFonts.StatusFontFamily,
                        SystemFonts.StatusFontStyle,
                        SystemFonts.StatusFontWeight,
                        new FontStretch());
                    var formattedText = new FormattedText(
                        txt,
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        defaultTypeface,
                        SystemFonts.StatusFontSize,
                        Brushes.Black)
                    {
                        // 文本最大宽度为线的宽度
                        MaxTextWidth = vec.Length,

                        // 设置文本对齐方式
                        TextAlignment = this.TextAlignment
                    };

                    var offsetY = this.StrokeThickness;
                    if (this.IsTextUp)
                    {
                        // 计算文本的行数
                        double textLineCount = formattedText.Width / formattedText.MaxTextWidth;
                        if (textLineCount < 1)
                        {
                            // 怎么也得有一行
                            textLineCount = 1;
                        }

                        // 计算朝上的偏移
                        offsetY = (-formattedText.Height * textLineCount) - this.StrokeThickness;
                    }

                    startPoint = startPoint + new Vector(0, offsetY);
                    drawingContext.DrawText(formattedText, startPoint);
                    drawingContext.Pop();
                }
            }
        }

        #endregion Overrides

        #region Private Methods

        /// <summary>
        /// 获取两个点的倾角
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <returns>两个点的倾角</returns>
        private double GetAngle(Point start, Point end)
        {
            var vec = end - start;

            // X轴
            var xaxis = new Vector(1, 0);
            return Vector.AngleBetween(xaxis, vec);
        }

        #endregion Private Methods
    }
}
