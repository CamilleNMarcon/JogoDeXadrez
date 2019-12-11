using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        private PartidaXadrez partida;
        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida ) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }
        public override string ToString()
        {
            return "P ";
        }
        private bool existeInimigo(Posicao proxima)
        {
            Peca P = tabuleiro.peca(proxima);
            return P != null && P.cor != cor;
        }

        private bool livre(Posicao proxima)
        {
            return tabuleiro.peca(proxima) == null;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao proxima = new Posicao(0, 0);

            //

            if (cor == Cor.Branca)
            {
                proxima.defValores(posicao.linhas - 1, posicao.colunas);
                if (tabuleiro.posicaoValida(proxima) && livre(proxima))
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }


                proxima.defValores(posicao.linhas - 2, posicao.colunas);
                if (tabuleiro.posicaoValida(proxima) && QuantMovimentos == 0)
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }

                proxima.defValores(posicao.linhas - 1, posicao.colunas - 1);
                if (tabuleiro.posicaoValida(proxima) && existeInimigo(proxima))
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }

                proxima.defValores(posicao.linhas - 1, posicao.colunas + 1);
                if (tabuleiro.posicaoValida(proxima) && existeInimigo(proxima))
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }

                // #jogadaEspecial en Passant

            if(posicao.linhas == 3)
                {
                    Posicao esquerda = new Posicao(posicao.linhas, posicao.colunas - 1);
                    if (tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && tabuleiro.peca(esquerda) == partida.vulneravelEnPassant){
                        mat[esquerda.linhas - 1, esquerda.colunas] = true;
                    }
                    Posicao direita = new Posicao(posicao.linhas, posicao.colunas + 1);
                    if (tabuleiro.posicaoValida(direita) && existeInimigo(direita) && tabuleiro.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linhas - 1, direita.colunas] = true;
                    }
                }

            }
            else
            {
                proxima.defValores(posicao.linhas + 1, posicao.colunas);
                if (tabuleiro.posicaoValida(proxima) && livre(proxima))
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }


                proxima.defValores(posicao.linhas + 2, posicao.colunas);
                if (tabuleiro.posicaoValida(proxima) && QuantMovimentos == 0)
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }

                proxima.defValores(posicao.linhas + 1, posicao.colunas - 1);
                if (tabuleiro.posicaoValida(proxima) && existeInimigo(proxima))
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }

                proxima.defValores(posicao.linhas + 1, posicao.colunas + 1);
                if (tabuleiro.posicaoValida(proxima) && existeInimigo(proxima))
                {
                    mat[proxima.linhas, proxima.colunas] = true;
                }

                // #jogadaEspecial en Passant

                if (posicao.linhas == 4)
                {
                    Posicao esquerda = new Posicao(posicao.linhas, posicao.colunas - 1);
                    if (tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && tabuleiro.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.linhas + 1, esquerda.colunas] = true;
                    }
                    Posicao direita = new Posicao(posicao.linhas, posicao.colunas + 1);
                    if (tabuleiro.posicaoValida(direita) && existeInimigo(direita) && tabuleiro.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linhas + 1, direita.colunas] = true;
                    }
                }

            }
            return mat;
        }
            
            
        
    }
}
