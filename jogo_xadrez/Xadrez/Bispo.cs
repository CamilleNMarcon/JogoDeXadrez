using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }
        public override string ToString()
        {
            return "B ";
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

            
           //acima da posição do bispo(noroeste):

            proxima.defValores(posicao.linhas - 1, posicao.colunas - 1);
            while (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
                if(tabuleiro.peca(proxima) != null && tabuleiro.peca(proxima).cor != cor)
                {
                    break;
                }
                proxima.defValores(proxima.linhas - 1, proxima.colunas - 1);
            }

            //acima da posição do bispo (nordeste):

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

            //acima da posição do bispo (suldeste):

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

            //acima da posição do bispo (suldoeste):

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
