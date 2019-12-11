
namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }

        // Matriz de Peças
        // Private - não pode ser acessada por ninguem de fora

        private Peca[,] pecas;

        public Tabuleiro (int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;

            // Recebe novas Linhas e Colunas

            pecas = new Peca[linhas, colunas];
        }

        public Peca peca( int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca peca( Posicao proxima)
        {
            return pecas[proxima.linhas, proxima.colunas];
        }

        public bool existePeca(Posicao proxima)
        {
            validarPosicao(proxima);
            return peca(proxima) != null;
        }

        //void =

        public void colocarPeca(Peca P, Posicao proxima)
        {
            if (existePeca(proxima))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            pecas[proxima.linhas, proxima.colunas] = P;
            P.posicao = proxima;
        }

        public Peca retirarPeca(Posicao proxima)
        {
            if (peca(proxima) == null)
            {
                return null;
            }

            Peca paraAPosicao = peca(proxima);
            paraAPosicao.posicao = null;
            pecas[proxima.linhas, proxima.colunas] = null;
            return paraAPosicao;
        }

        public bool posicaoValida(Posicao proxima)
        {
            if(proxima.linhas < 0 || proxima.linhas >= linhas || proxima.colunas < 0 || proxima.colunas >= colunas)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void validarPosicao(Posicao proxima)
        {
            if (!posicaoValida(proxima))
            {
                throw new TabuleiroException("Posição Invalida!");
            }
        }
    }
}
