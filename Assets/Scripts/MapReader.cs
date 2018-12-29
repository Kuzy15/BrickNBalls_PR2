using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReader : MonoBehaviour {


    TextAsset map;

    string _header1;
    string _header2;
    int _itermediate;
    int _index;
    int _indexHits;
    string _text;
    int[] _typeBlock;
    int _typeIdx;
    int[] _hitsBlock;
    int _hitsIdx;

    public MapReader(TextAsset _map)
    {
        map = _map;
    }

    public void Reader(ref int[] typeBlock, ref int[] hitBlock)
    {

        _header1 = "[layer]\r\ntype=Tile Layer 1\r\ndata=\r\n";
        _header2 = "[layer]\r\ntype=Tile Layer 2\r\ndata=\r\n";
        _itermediate = 2;

        _index = _header1.Length;
        _indexHits = _index + (11 * _itermediate) + (11 * 11 * 2) + _header2.Length;

        _text = map.text;


        _typeBlock = new int[11 * 11];
        _typeIdx = 0;
        _hitsBlock = new int[11 * 11];
        _hitsIdx = 0;

        string acc = "";
        string accHits = "";


        while (_indexHits < _text.Length)
        {
            if (_text[_index] != ',' && _text[_index] != '.')
            {
                acc += _text[_index];
                _index++;
            }
            if (_text[_indexHits] != ',' && _text[_indexHits] != '.')
            {
                accHits += _text[_indexHits];
                _indexHits++;
            }
            if ((_text[_index] == ',' || _text[_index] == '.') && (_text[_indexHits] == ',' || _text[_indexHits] == '.'))
            {
                _typeBlock[_typeIdx] = int.Parse(acc);
                _typeIdx++;
                _hitsBlock[_hitsIdx] = int.Parse(accHits);
                _hitsIdx++;
                _index++;
                _indexHits++;
                acc = "";
                accHits = "";
            }
            if (_text[_index] == '\r')
            {
                _index += 2;
            }
            if (_text[_indexHits] == '\r')
            {
                _indexHits += 2;
            }
        }
        typeBlock = _typeBlock;
        hitBlock = _hitsBlock;
    }
}
