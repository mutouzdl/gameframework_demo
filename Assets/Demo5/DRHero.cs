using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace Demo5 {
    public class DRHero : IDataRow {
        public int Id { get; protected set; }
        public string Name { get; private set; }
        public int Atk { get; private set; }

        public void ParseDataRow (string dataRowText) {
            string[] text = dataRowText.Split ('\t');
            int index = 0;
            index++; // 跳过#注释列

            Id = int.Parse (text[index++]);
            Name = text[index++];
            Atk = int.Parse (text[index++]);
        }
    }
}