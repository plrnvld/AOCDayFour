using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program {
    public static void Main (string[] args) 
    {
        var bingoPlayer = new BingoPlayer();

        bingoPlayer.ReadLines("Input.txt");
        bingoPlayer.StartPlaying();        
    }
}