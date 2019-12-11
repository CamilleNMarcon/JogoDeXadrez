using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }
        public override string ToString()
        {
            return "C ";
        }
        private bool podeMover(Posicao proxima)
        {
            Peca P = tabuleiro.peca(proxima);
            return P == null || P.cor != cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao proxima = new Posicao(0, 0);


            //L:

            proxima.defValores(posicao.linhas - 1, posicao.colunas - 2);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas - 2, posicao.colunas - 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas - 2, posicao.colunas + 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas - 1, posicao.colunas + 2);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas + 1, posicao.colunas + 2);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas + 2, posicao.colunas + 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas + 2, posicao.colunas - 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //L:

            proxima.defValores(posicao.linhas + 1, posicao.colunas - 2);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }



            return mat;
        }
    }
}
