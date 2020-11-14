using System.Collections.Generic;

namespace Tabowo.misc
{

public class TabowoScriptInterpreter
{
    //version 1.0.0
    //Main target: is to use IndexOf() instead of always substring 1 byte
    //Maybe this method can be faster than doing this like that: substring for 1 byte at index_position and then check some ifs


    public List<string> arguments = new List<string>();             //arguments
    public string source;                                           //Text source
    public int index = 0;                                           //current interpreter position          (to reset simply set to 0)
    const int unreachable= 2147483647;

    public void Next()
    {
        // , as arg
        // ; as end
        // @ as skip

        arguments.Clear();
        arguments.Add("");                          //one for backup
        int arg, end, skip;
        ushort dec;


        while(true)
        {
            end = source.IndexOf(';', index);
            if (end == -1)
            {
                arguments.Clear();
                return;                              //nothing to do
            }
            skip = source.IndexOf('@', index);
            if (skip == -1) skip = unreachable;      //if it is nowhere then set to the unreachable value
            arg = source.IndexOf(',', index);
            if (arg == -1) arg = unreachable;        //same here

            dec = Closest(arg, end, skip);

            if (dec == 1)                                       //arg
            {
                Paste(source.Substring(index, arg - index));
                arguments.Add("");          //backup
                index = arg;
            }
            else if (dec == 2)                                  //end
            {
                Paste(source.Substring(index, end - index));
                index = end + 1;
                return;
            }
            else if (dec == 3)                                  //skip
            {
                Paste(source.Substring(index, skip - index) );
                Paste(source.Substring(skip+1, 1));
                index = skip +1;
            }

            index++;
        }
        

    }


    private void Paste(string s)
    {
        arguments[arguments.Count - 1] += s;
    }

    //i want to know which number is the closest, i don't want to know the value of the smallest
    private ushort Closest(int a,int b,int c)
    {
        if (a < b)
            if (a < c) return 1;
            else return 3;
            if (b < c) return 2;
            return 3;
    }



}
}
