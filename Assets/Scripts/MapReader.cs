using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapReader
{
    private TextAsset _map;
    private string _header1;
    private string _header2;
    private int _index;
    private string _text;
    private List<int> _blockList = new List<int>();

    public MapReader(TextAsset map)
    {
        _map = map;
    }

    public void Reader(ref List<int> blockList)
    {

        _header1 = "[layer]\r\ntype=Tile Layer 1\r\ndata=\r\n";
        _header2 = "[layer]\r\ntype=Tile Layer 2\r\ndata=\r\n";

        _index = _header1.Length;
        _text = _map.text;

        string acc = "";

        while (_index < _text.Length)
        {

            if (_text[_index] != ',')
            {
                if(_text[_index] == '\r')
                {
                    _index += 2;
                }
                acc += _text[_index];
                _index++;
            }
            if (_text[_index] == ',' || _text[_index] == '.')
            {
                _blockList.Add(int.Parse(acc));
                if (_text[_index] == ',')
                {
                    _index++;
                }
                if (_text[_index] == '.')
                {
                    _index += _header2.Length + 1;
                }
                acc = "";
            }
        }
        blockList = _blockList;
    }

}
