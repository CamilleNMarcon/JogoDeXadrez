using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {

        private PartidaXadrez partida;
        
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }
        public override string ToString()
        {
            return "R ";
        }

        //REI - Pode mover 1 unica casa para todas as direçoes

        private bool podeMover (Posicao proxima)
        {
            Peca P = tabuleiro.peca(proxima);
            return P == null || P.cor != cor;
        }

        private bool testeTorreRoque(Posicao proxima)
        {
            Peca P = tabuleiro.peca(proxima);
            return P != null && P is Torre && P.cor == cor && P.QuantMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao proxima = new Posicao(0, 0);

            //acima da posição do rei:

            proxima.defValores(posicao.linhas - 1, posicao.colunas);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //acima da posição do rei(nordeste):

            proxima.defValores(posicao.linhas - 1, posicao.colunas + 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //direita da posição do rei:

            proxima.defValores(posicao.linhas, posicao.colunas + 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //suldeste da posição do rei:

            proxima.defValores(posicao.linhas + 1, posicao.colunas + 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //abaixo da posição do rei:

            proxima.defValores(posicao.linhas + 1, posicao.colunas);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //suldoeste da posição do rei:

            proxima.defValores(posicao.linhas + 1, posicao.colunas - 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //Esquerda da posição do rei:

            proxima.defValores(posicao.linhas, posicao.colunas - 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            //noroeste da posição do rei:

            proxima.defValores(posicao.linhas - 1, posicao.colunas - 1);
            if (tabuleiro.posicaoValida(proxima) && podeMover(proxima))
            {
                mat[proxima.linhas, proxima.colunas] = true;
            }

            // #jogadaEspecial roque

            if (QuantMovimentos == 0 && !partida.xeque)
            {
                //roque pequeno:

                Posicao posicaoTorre1 = new Posicao(posicao.linhas, posicao.colunas + 3);
                if (testeTorreRoque(posicaoTorre1)) 
                {
                    Posicao P1 = new Posicao(posicao.linhas, posicao.colunas + 1);
                    Posicao P2 = new Posicao(posicao.linhas, posicao.colunas + 2);
                    if (tabuleiro.peca(P1) == null && tabuleiro.peca(P2) == null)
                    {
                        mat[posicao.linhas, posicao.colunas + 2] = true;
                    }
                }

                //roque grande:

                Posicao posicaoTorre2 = new Posicao(posicao.linhas, posicao.colunas - 4);
                if (testeTorreRoque(posicaoTorre1))
                {
                    Posicao P1 = new Posicao(posicao.linhas, posicao.colunas - 1);
                    Posicao P2 = new Posicao(posicao.linhas, posicao.colunas - 2);
                    Posicao P3 = new Posicao(posicao.linhas, posicao.colunas - 3);
                    if (tabuleiro.peca(P1) == null && tabuleiro.peca(P2) == null && tabuleiro.peca(P3) == null)
                    {
                        mat[posicao.linhas, posicao.colunas - 2] = true;
                    }
                }
            }


            return mat;
        }

    }
}
   

