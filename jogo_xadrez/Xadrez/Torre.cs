using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Torre : Peca
    {
        //TORRE -
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {


        }
        public override string ToString()
        {
            return "T ";
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

            //acima da posição da torre:

            proxima.defValores(posicao.linhas - 1, posicao.colunas);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.linhas = proxima.linhas - 1;
            }

            //abaixo da posição da torre:

            proxima.defValores(posicao.linhas + 1, posicao.colunas);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.linhas = proxima.linhas + 1;
            }

            //direita da posição da torre:

            proxima.defValores(posicao.linhas, posicao.colunas + 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.colunas = proxima.colunas + 1;
            }

            //esquerda da posição da torre:

            proxima.defValores(posicao.linhas, posicao.colunas - 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.colunas = proxima.colunas - 1;
            }

            return mat;


        }
    }
}
