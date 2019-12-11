using Xadrez;
using System;
using tabuleiro;

namespace jogo_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {

                PartidaXadrez partida = new PartidaXadrez();
                while (!partida.partidaTerminada)
                {
                    try {
                        Console.Clear();
                        Tela.imprimirPartida(partida);
                       

                        Console.WriteLine();
                        Console.Write("Posição Inicial:");
                        Posicao inicial = Tela.lerPosicaoXadrez().paraAPosicao();
                        partida.validarPosicaoInicial(inicial);

                        bool[,] posicoesPossiveis = partida.tabuleiro.peca(inicial).movimentosPossiveis();

                        Console.Clear();
                        Tela.ImagemTabuleiro(partida.tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Posição Final:");
                        Posicao final = Tela.lerPosicaoXadrez().paraAPosicao();
                        partida.validarPosicaoFinal(inicial, final);


                        partida.realizaJogada(inicial, final);


                    }

                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Tela.imprimirPartida(partida);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
    }
}

  /*Console.WriteLine(proxima.paraAPosicao());
        Console.WriteLine(proxima);*/
        

            
          
    

