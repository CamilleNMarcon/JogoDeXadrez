using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool partidaTerminada { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            partidaTerminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao inicial, Posicao final)
        {
            Peca P = tabuleiro.retirarPeca(inicial);
            P.IncrementaQtdMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(final);
            tabuleiro.colocarPeca(P, final);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            // #jogadaEspecial roque pequeno
            if(P is Rei && final.colunas == inicial.colunas + 2)
            {
                Posicao inicialTorre = new Posicao(inicial.linhas, inicial.colunas + 3);
                Posicao finalTorre = new Posicao(inicial.linhas, inicial.colunas + 1);
                Peca T = tabuleiro.retirarPeca(inicialTorre);
                T.IncrementaQtdMovimentos();
                tabuleiro.colocarPeca(T, finalTorre);
            }

            // #jogadaEspecial roque grande
            if (P is Rei && final.colunas == inicial.colunas - 2)
            {
                Posicao inicialTorre = new Posicao(inicial.linhas, inicial.colunas - 4);
                Posicao finalTorre = new Posicao(inicial.linhas, inicial.colunas - 1);
                Peca T = tabuleiro.retirarPeca(inicialTorre);
                T.IncrementaQtdMovimentos();
                tabuleiro.colocarPeca(T, finalTorre);
            }

            // #jogadaEspecail en Passant

            if(P is Peao)
            {
                if (inicial.colunas != final.colunas && pecaCapturada == null)
                {
                    Posicao PosPeao;
                    if(P.cor == Cor.Branca)
                    {
                        PosPeao = new Posicao(final.linhas + 1, final.colunas);
                    }
                    else
                    {
                        PosPeao = new Posicao(final.linhas - 1, final.colunas);
                    }
                    pecaCapturada = tabuleiro.retirarPeca(PosPeao);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento (Posicao inicial, Posicao final, Peca pecaCapturada)
        {
            Peca P = tabuleiro.retirarPeca(final);
            P.decrementaQtdMovimentos();
            if(pecaCapturada != null)
            {
                tabuleiro.colocarPeca(pecaCapturada, final);
                capturadas.Remove(pecaCapturada);
            }
            tabuleiro.colocarPeca(P, inicial);

            // #jogadaEspecial roque pequeno
            if (P is Rei && final.colunas == inicial.colunas + 2)
            {
                Posicao inicialTorre = new Posicao(inicial.linhas, inicial.colunas + 3);
                Posicao finalTorre = new Posicao(inicial.linhas, inicial.colunas + 1);
                Peca T = tabuleiro.retirarPeca(inicialTorre);
                T.decrementaQtdMovimentos();
                tabuleiro.colocarPeca(T, inicialTorre);
            }

            // #jogadaEspecial roque grande
            if (P is Rei && final.colunas == inicial.colunas - 2)
            {
                Posicao inicialTorre = new Posicao(inicial.linhas, inicial.colunas - 4);
                Posicao finalTorre = new Posicao(inicial.linhas, inicial.colunas - 1);
                Peca T = tabuleiro.retirarPeca(inicialTorre);
                T.IncrementaQtdMovimentos();
                tabuleiro.colocarPeca(T, finalTorre);
            }

            //#jogadaEspecial en Passant
            if(P is Peao)
            {
                if(inicial.colunas != final.colunas && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tabuleiro.retirarPeca(final);
                    Posicao posPeao;
                    if(P. cor == Cor.Branca)
                    {
                        posPeao = new Posicao(3, final.colunas);
                    }
                    else
                    {
                        posPeao = new Posicao(4, final.colunas);
                    }

                    tabuleiro.colocarPeca(peao, posPeao);
                }
            }
            
        }
        public void realizaJogada (Posicao inicial, Posicao final)
        {
            Peca pecaCapturada = executaMovimento(inicial, final);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(inicial, final, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca P = tabuleiro.peca(final);

            //#jogadaEspecial Promocao

            if(P is Peao)
            {
                if ((P.cor == Cor.Branca && final.linhas == 0) || (P.cor == Cor.Preta && final.linhas == 7))
                {
                    P = tabuleiro.retirarPeca(final);
                    pecas.Remove(P);
                    Peca dama = new Dama(tabuleiro, P.cor);
                    tabuleiro.colocarPeca(dama, final);
                    pecas.Add(dama);
                }
            }

            
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual)))
            {
                partidaTerminada = true;
            }
            else
            {
                turno++;
                mudarJogador();
            }
            

            // #jogadaEspecial en Passant
            if(P is Peao && (final.linhas == inicial.linhas -2 || final.linhas == inicial.linhas + 2))
            {
                vulneravelEnPassant = P;
            }
            else
            {
                vulneravelEnPassant = null;
            }
            
        }

        public void validarPosicaoInicial (Posicao proxima)
        {
            if(tabuleiro.peca(proxima) == null)
            {
                throw new TabuleiroException("Não existe peça na posição inicial escolhida!");
            }
            if(jogadorAtual != tabuleiro.peca(proxima).cor)
            {
                throw new TabuleiroException("Essa peça inicial escolhida, não é sua!");
            }
            if (!tabuleiro.peca(proxima).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça inicial escolhida!");
            }
        }

        public void validarPosicaoFinal(Posicao inicial, Posicao final)
        {
            if (!tabuleiro.peca(inicial).movimentoPossivel(final))
            {
                throw new TabuleiroException("Posição final invalida!");
            }
        }

        private void mudarJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
                
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }

            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria (Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            //problema - rei esta dando null
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
           return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);

            if(R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if(mat[R.posicao.linhas, R.posicao.colunas])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i=0; i<tabuleiro.linhas; i++)
                {
                    for(int j=0; j<tabuleiro.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao inicial = x.posicao;
                            Posicao final = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(inicial, final);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(inicial, final, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).paraAPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            //BRANCAS:
            colocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branca, this));


            //PRETAS:
            colocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preta, this));


        }
    } 


}
