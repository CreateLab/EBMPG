// See https://aka.ms/new-console-template for more information

using EBMPG.Generator;
using EBMPG.Model;

Console.WriteLine("Hello, World!");

/*var readLines = File.ReadLines("./Test.txt");
Console.WriteLine(readLines.Count());
Console.WriteLine("----------------------------------");
foreach (var line in readLines)
{
    Console.WriteLine(line.Length);
}*/
/*var list = new List<string>();
foreach (var line in readLines)
{
    if (line.Length == 51)
    {
        var r = line.Take(50);
        list.Add(new string(r.ToArray()));
    }
    else
    {
        list.Add(line);
    }

}

File.WriteAllLines("./Test.txt", list);*/

var generator = new GeneratorBmp();

var text = File.ReadAllLines("./Test.txt").ToArray();

var image = new Pixel[text.Length, text.First().Length];

//  right cross

/*for (var i = 0; i < image.GetLength(0); i++)
{
    for (var j = 0; j < image.GetLength(1); j++)
    {
        
        if(i == j)
            image[i, j] = new Pixel
            {
                R = 255,
                G = 255,
                B = 255
            };
        else
            image[i, j] = new Pixel
            {
                R = 0,
                G = 0,
                B = 0
            };
        
      
    }
}*/

/*//red square
image[0, 0] = new Pixel(R: 255);
image[0, 1] = new Pixel(R: 255);
image[1, 0] = new Pixel(R: 255);
image[1, 1] = new Pixel(R: 255);

// green square
image[14, 0] = new Pixel(G: 255);
image[15, 0] = new Pixel(G: 255);
image[14, 1] = new Pixel(G: 255);
image[15, 1] = new Pixel(G: 255);

//blue square
image[0, 14] = new Pixel(B: 255);
image[0, 15] = new Pixel(B: 255);
image[1, 14] = new Pixel(B: 255);
image[1, 15] = new Pixel(B: 255);

// orange square 4 * 4 in the middle
image[8, 8] = new Pixel
{
    R = 255,
    G = 165,
    B = 0
};
image[8, 9] = new Pixel
{
    R = 255,
    G = 165,
    B = 0
};
image[9, 8] = new Pixel
{
    R = 255,
    G = 165,
    B = 0
};
image[9, 9] = new Pixel
{
    R = 255,
    G = 165,
    B = 0
};*/



for (var i = 0; i < image.GetLength(0); i++)
{
    for (var j = 0; j < image.GetLength(1); j++)
    {
        if (text[i][j] == '*')
        {
            image[i, j] = new Pixel
            {
                R = 255,
                G = 0,
                B = 0
            };
        }
        else
        {
            image[i, j] = new Pixel
            {
                R = 255,
                G = 255,
                B = 255
            };
        }
    }
}

var bmp = generator.Generate(image);

File.WriteAllBytes("test.bmp", bmp);