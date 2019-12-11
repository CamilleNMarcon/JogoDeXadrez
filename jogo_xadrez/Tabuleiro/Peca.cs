
namespace tabuleiro
{
    //Peças do Tabuleiro
    // Protected = acessada somente pelas sub classes
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }

        public int QuantMovimentos { get; set; }
        public Tabuleiro tabuleiro { get; set; }


        public Peca (Tabuleiro tabuleiro, Cor cor)
        {
            this.posicao = null;
            this.tabuleiro = tabuleiro;
            this.cor = cor;
            this.QuantMovimentos = 0;
        }

        public void IncrementaQtdMovimentos() 
        {
            QuantMovimentos++;
        }

        public void decrementaQtdMovimentos()
        {
            QuantMovimentos--;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for( int  i=0; i<tabuleiro.linhas; i++)
            {
                for(int j=0; j<tabuleiro.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool movimentoPossivel(Posicao proxima)
        {
            return movimentosPossiveis()[proxima.linhas, proxima.colunas];
        }
        public abstract bool[,] movimentosPossiveis();
    }
}
