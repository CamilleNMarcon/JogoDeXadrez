using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }
        public override string ToString()
        {
            return "D ";
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

            //esquerda da posição da dama:

            proxima.defValores(posicao.linhas, posicao.colunas - 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas, proxima.colunas - 1);
            }

            //direita da posição da dama:

            proxima.defValores(posicao.linhas, posicao.colunas + 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas, proxima.colunas + 1);
            }

            //acima da posição da dama:

            proxima.defValores(posicao.linhas - 1, posicao.colunas);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas - 1, proxima.colunas);
            }

            //abaixo da posição da dama:

            proxima.defValores(posicao.linhas + 1, posicao.colunas);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas + 1, proxima.colunas);
            }

            //noroeste da posição da dama:

            proxima.defValores(posicao.linhas - 1, posicao.colunas - 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas - 1, proxima.colunas - 1);
            }

            //nordeste da posição da dama:

            proxima.defValores(posicao.linhas - 1, posicao.colunas + 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas - 1, proxima.colunas + 1);
            }

            //suldeste da posição da dama:

            proxima.defValores(posicao.linhas + 1, posicao.colunas + 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas + 1, proxima.colunas + 1);
            }

            //suldoeste da posição da dama:

            proxima.defValores(posicao.linhas + 1, posicao.colunas - 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if (tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas + 1, proxima.colunas - 1);
            }


            return mat;

        }
    }
}
