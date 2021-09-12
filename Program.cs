using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepository repository = new SerieRepository();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string opcao = ObterOpacaoUsuario();

            while (opcao.ToUpper() != "X")
            {
                switch (opcao)
                {
                    
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                    throw new ArgumentOutOfRangeException();                    
                }
                opcao = ObterOpacaoUsuario().ToUpper();
            }
            Console.WriteLine("Thank you for using ours services! Goodbye!");
            Console.ReadLine();
        }

        private static void VisualizarSerie()
        {
             Console.WriteLine("Visualizar Série");
            Console.WriteLine();
            Console.Write("Digite o Id da Série que você quer Listar: ");
            int listaId = int.Parse(Console.ReadLine());
            //int id, Serie objeto
            var serie = repository.RetornaPorId(listaId);
            if (serie.Id != listaId)
            {
                Console.WriteLine("Série não localizada!");
                return;
            } else
            {
                Console.WriteLine(serie);
            } 
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Excluir Série");
            Console.WriteLine();
            Console.Write("Digite o Id da Série que você quer Excluir: ");
            int listaId = int.Parse(Console.ReadLine());
            var lista = repository.Lista();
            if (lista.Count < listaId-1){
                Console.WriteLine("Série não localizada!");
                return;
            } else {
                Console.Write("Tem certezada que deseja excluir a série {0}? (s/n)",lista[listaId].RetornaTitulo());
                string confirmacao = Console.ReadLine().ToUpper();
                if (confirmacao == "S")
                {
                    repository.Excluir(listaId);
                    Console.WriteLine("Série {0} excluida com sucesso (s/n)",lista[listaId].RetornaTitulo());
                }
            }

        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar Série");
            Console.WriteLine();
            Console.Write("Digite o Id da Série que você quer alterar: ");
            int listaId = int.Parse(Console.ReadLine());
            //int id, Serie objeto
            var lista = repository.Lista();
            if (lista.Count < listaId-1)
            {
                Console.WriteLine("Série não localizada!");
                return;
            } else
            {
                int entradaGenero = 0;
                bool validaGenero = false;
                while (!validaGenero)
                {
                    Console.WriteLine("Generos Disponíveis: ");
                    foreach(int i in Enum.GetValues(typeof(Genero)))
                    Console.WriteLine("{0,3} - {1}",
                            (int) i, ((Genero) i));
                    Console.Write("Digite o novo Genero: ");
                    entradaGenero = int.Parse(Console.ReadLine());
                    if ((entradaGenero > 0) && (entradaGenero < 14)) 
                    {
                        validaGenero = true;
                    } else {
                        Console.WriteLine("Gênero Inválido, escolha entre 1 e {0}", Enum.GetValues(typeof(Genero)).Length );
                    }
                }
                Console.Write("Digite o novo Título: ");
                string titulo = Console.ReadLine();
                Console.Write("Digite a nova Descrição: ");
                string descricao = Console.ReadLine();
                Console.Write("Digite o novo Ano de Lançamento: ");
                int ano = int.Parse(Console.ReadLine());
                Serie serie = new Serie(listaId, genero:(Genero) entradaGenero, titulo, descricao, ano);
                repository.Atualiza(listaId, serie);
                Console.WriteLine("Série {0} atualizada com sucesso!", titulo);
            }           
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir Séries");

            int entradaGenero = 0;
            bool validaGenero = false;
            while (!validaGenero)
            {
                Console.WriteLine("Digite o Genero: ");
                foreach(int i in Enum.GetValues(typeof(Genero)))
                Console.WriteLine("{0,3} - {1}",
                           (int) i, ((Genero) i));
                entradaGenero = int.Parse(Console.ReadLine());
                if ((entradaGenero > 0) && (entradaGenero < 14)) 
                {
                    validaGenero = true;
                } else {
                    Console.WriteLine("Gênero Inválido, escolha entre 1 erídica");
                }
            }
            Console.WriteLine("Digite o Título: ");
            string titulo = Console.ReadLine();
            Console.WriteLine("Digite a Descrição: ");
            string descricao = Console.ReadLine();
            Console.WriteLine("Digite o Ano de Lançamento: ");
            int ano = int.Parse(Console.ReadLine());
            //int listaId = repository.ProximoId();
            Serie serie = new Serie(id: repository.ProximoId(), 
                genero: (Genero)entradaGenero, 
                titulo: titulo, 
                descricao: descricao, 
                ano: ano);

            repository.Insere(serie);
            Console.WriteLine("Série {0} inserida com sucesso!", titulo);
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar Séries");

            var lista = repository.Lista();

            if (lista.Count ==0)
            {
                Console.WriteLine("Lista de séries está vazia!");
                return;
            } else
            {
                foreach (var serie in lista)
                {
                    if (!serie.ValidaExluido())
                    {
                        Console.WriteLine("#ID {0} - {1}", serie.RetornaId(), serie.RetornaTitulo());
                    }
                    
                }
            }
        }

        private static string ObterOpacaoUsuario(){
            Console.WriteLine("DIO Series ao seu dispor!");
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return opcaoUsuario;
        }
        
    }
}
