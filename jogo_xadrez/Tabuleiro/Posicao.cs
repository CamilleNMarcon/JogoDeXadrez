﻿
namespace tabuleiro
{
    class Posicao
    {
        public int linhas { get; set; }
        public int colunas { get; set; }

        // this = 
        public Posicao(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
        }

        public void defValores(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
        }

        // override =

        public override string ToString()
        {
            return linhas + ", " + colunas;
        }
    }
}
