﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BrownianMotion
{
    class DrawData
    {
        #region Fields

        private readonly List<Ellipse> _dotList;
        private double _height;
        private Point _lastClick;
        private readonly int _radius;
        private double _width;
        
        #endregion

        #region Dictionaries

        private readonly Dictionary<string, PropertyPath> _propertiesList;
        private readonly Dictionary<string, Func<double>> _maxValuesDictionary;
        private readonly Dictionary<string, Func<Ellipse, double>> _beginValuesDictionary;
        private readonly Dictionary<string, Func<Ellipse, double, bool>> _relocateValuesDictionary;
        
        #endregion

        #region Public Properties

        public Ellipse Dot
        {
            get => _dotList.Last();
            private set => _dotList.Add(value);
        }

        #endregion

        #region Constructor

        public DrawData()
        {
            _radius = 5;
            _dotList = new List<Ellipse>();
            _lastClick = new Point(0, 0);

            _propertiesList = new Dictionary<string, PropertyPath>
            {
                {"x", new PropertyPath(Canvas.LeftProperty)},
                {"y", new PropertyPath(Canvas.TopProperty)}
            };
            _maxValuesDictionary = new Dictionary<string, Func<double>> {{"x", () => _width}, {"y", () => _height}};
            _beginValuesDictionary = new Dictionary<string, Func<Ellipse, double>> {{"x", Canvas.GetLeft}, {"y", Canvas.GetTop}};
            _relocateValuesDictionary = new Dictionary<string, Func<Ellipse, double, bool>>
            {
                {"x", (obj, value) => {Canvas.SetLeft(obj, value); return true;}},
                {"y", (obj, value) => {Canvas.SetTop(obj, value); return true;}}
            };
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            CreateDot();
            AddAnimation(Dot, "x");
            AddAnimation(Dot, "y");
        }

        public void FormSizeChange(Point formSize)
        {
            _width = formSize.X;
            _height = formSize.Y;
        }

        public void SetLastClick(Point click) => _lastClick = click;

        #endregion

        #region Private Methods

        private void AddAnimation(Ellipse dot, string animatedValue)
        {
            var story = new Storyboard();
            _propertiesList.TryGetValue(animatedValue, out var property);
            _maxValuesDictionary.TryGetValue(animatedValue, out var maxValue);
            _beginValuesDictionary.TryGetValue(animatedValue, out var beginValue);
            
            DoubleAnimation da = Animator.CreateDoubleAnimation(beginValue.Invoke(dot), maxValue.Invoke());

            da.Completed += (sender, args) =>
            {
                _relocateValuesDictionary.TryGetValue(animatedValue, out var setNewValue);
                setNewValue.Invoke(dot, da.To ?? 0);
                AddAnimation(dot, animatedValue);
            };

            Animator.ApplyAnimation(dot, da, property);
            story.Children.Add(da);
            story.Begin();
        }

        private void CreateDot() => Dot = Spot.CreateDot(_radius, Brushes.Black, _lastClick);

        #endregion
    }
}
