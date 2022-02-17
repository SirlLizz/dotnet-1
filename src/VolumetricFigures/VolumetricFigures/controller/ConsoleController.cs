﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using VolumetricFigures.model;
using VolumetricFigures.model.figures;

namespace VolumetricFigures.controller
{
    public class ConsoleController
    {

        private List<Counting> _figures = null;

        public ConsoleController(List<Counting> figures)
        {
            _figures = figures;
        }

        public void AddRectangularCuboid(int index, Point p1, Point p2)
        {
            AddFigure(index, new RectangularCuboid(p1, p2));
        }

        public void AddSphere(int index, Point p, double radius)
        {
            AddFigure(index, new Sphere(p, radius));
        }

        public void AddСylinder(int index, Point p, double radius, double height)
        {
            AddFigure(index, new Cylinder(p, radius, height));
        }

        public void AddFigure(int index, Counting figure)
        {
            if (_figures.Count != index)
            {
                DeleteFigure(index);
            }
            _figures.Insert(index, figure);
        }

        public int CompareSquare(int index1, int index2)
        {
            if (_figures[index1].GetSquare() > _figures[index2].GetSquare())
            {
                return index1;
            }
            return index2;
        }

        public int ComparePerimeter(int index1, int index2)
        {
            if (_figures[index1].GetPerimeter() > _figures[index2].GetPerimeter())
            {
                return index1;
            }
            return index2;
        }

        public void DeleteFigure(int index)
        {
            if (CheckIndex(index)) 
            {
                _figures.RemoveAt(index);
            }   
        }

        public void DeleteAll()
        {
            _figures.RemoveRange(0, _figures.Count);
        }

        public void OpenFile(String path)
        {
            try 
            { 
                XmlSerializer formatter = new XmlSerializer(typeof(List<Counting>));
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                _figures = (List<Counting>)formatter.Deserialize(fs);
            }
            catch (Exception ex)
            {
                Console.Write("File don't open\n");
                Console.ReadLine();
            }
        }

        public void SaveFile(String path)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Counting>));
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                formatter.Serialize(fs, _figures);
            }
            catch (Exception ex)
            {
                Console.Write("File don't save\n");
                Console.ReadLine();
            }

        }

        public Counting GetFigure(int index)
        {
            return _figures[index];
        }

        public List<Counting> GetFigures()
        {
            return _figures;
        }

        public void SetFigures(List<Counting> figures)
        {
            _figures = figures;
        }

        public bool CheckIndex(int index)
        {
            if (_figures != null)
            {
                if ((index < _figures.Count) && (index >= 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}