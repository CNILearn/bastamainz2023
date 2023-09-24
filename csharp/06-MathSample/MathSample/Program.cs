﻿using System.Numerics;

Point<int> pt1 = new(4, 5);
Transformation<int> tr1 = new(2, 3);
Point<int> pt2 = pt1 + tr1;  // move the point by the translation
Console.WriteLine($"original: {pt1}; moved: {pt2}");

Point<double> pt3 = new(X: 4.5, Y: 3.3);
Transformation<double> tr2 = new(XOffset: 2.2, YOffset: 3.3); 
Point<double> pt4 = pt3 + tr2;  // move the point by the translation
Console.WriteLine($"original: {pt3}; moved: {pt4}");

public record struct Transformation<T>(T XOffset, T YOffset) 
    where T : IAdditionOperators<T, T, T>
{
}

public record struct Point<T>(T X, T Y) 
    where T : IAdditionOperators<T, T, T>
{
    public static Point<T> operator +(Point<T> left, Transformation<T> right) =>
        left with { X = left.X + right.XOffset, Y = left.Y + right.YOffset };
}
