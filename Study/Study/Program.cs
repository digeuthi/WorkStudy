using System;

class Program

{
    enum DialogResult { YES = 10, NO, CANCLE, CONFIRM = 50, OK }; //enum은 메서드 밖에 선언되어야함.
    static void Main()
    {
        /*float a = 69.6875f;
        Console.WriteLine("a : {0}", a);

        double b = (double)a;
        Console.WriteLine("b : {0}", b);

        Console.WriteLine("69.6875 == b : {0}", 69.6875 == b);

        float x = 0.1f;
        Console.WriteLine("x : {0}", x);

        double y = (double)x;
        Console.WriteLine("y : {0}", y);

        Console.WriteLine("0.1 == y : {0}", 0.1 == y);

        int r = 123;
        string t = r.ToString();
        Console.WriteLine(t);

        float c = 3.14f;
        string d = c.ToString();
        Console.WriteLine(d);

        string e = "122345";
        int f = int.Parse(e);
        Console.WriteLine(f);

        string g = "1.2345";
        float h = float.Parse(g);*/

        const int MAX_INT = 12156483;
        const int MIN_INT = -12156483;
        /*MAX_INT = 12156484;*/
        Console.WriteLine(MAX_INT);
        Console.WriteLine(MIN_INT);

        Console.WriteLine((int)DialogResult.YES);
        Console.WriteLine((int)DialogResult.NO);
        Console.WriteLine((int)DialogResult.CANCLE);
        Console.WriteLine((int)DialogResult.CONFIRM);
        Console.WriteLine((int)DialogResult.OK);

        DialogResult result = DialogResult.YES;

        Console.WriteLine(result == DialogResult.YES);
        Console.WriteLine(result == DialogResult.NO);
        Console.WriteLine(result == DialogResult.CANCLE);
        Console.WriteLine(result == DialogResult.CONFIRM);
        Console.WriteLine(result == DialogResult.OK);

        int? n = null;
        Console.WriteLine(n.HasValue);
        Console.WriteLine(n != null);

        n = 3;
        Console.WriteLine(n.HasValue);
        Console.WriteLine(n != null);
        Console.WriteLine(n.Value);

        var a = 20;
        Console.WriteLine("Type : {0}, Value : {1}", a.GetType(), a);

        var b = 3.141142;
        Console.WriteLine("Type : {0}, Value : {1}", b.GetType(), b);

        var c = "Hello World";
        Console.WriteLine("Type : {0}, Value : {1}", c.GetType(), c);

        var d = new int[] { 10, 20, 30 };
        Console.Write("Type : {0}, Value : ", d.GetType());
        foreach (var e in d)
            Console.Write("{0} ", e);

        Console.WriteLine();
    }
}