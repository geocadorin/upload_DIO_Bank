using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        
        static List<Conta> listaContas = new List<Conta>();
        static void Main(string[] args)
        {
            /* Cria contas fakes so para testes para evitar ficar criando toda hora,
               uma vez que todos os dados são perdidos quando o sistema é fechado por não possuir banco
               de dados
            */ 

            //arr criado para colocar inserir nomes para as contas
            var nomes = new string[]
                {
                    "Jean-Claude Van Damme",
                    "Arnold Schwarzenegger",
                    "Amim Hamar Amado",
                    "La Casa de Pastel"
                };

            //para gerar valores aleatórios para serem usados no salário e crédito
            Random randNum = new Random();
            for(var i = 0; i < nomes.Length; i++){
                Conta novaConta = new Conta(
                    tipoConta: i <= 2 ? TipoConta.PessoaFisica : TipoConta.PessoaJuridica,
                    saldo: randNum.Next(1000, 5000),
                    credito: randNum.Next(100, 500),
                    nome: nomes[i]
                );
                listaContas.Add(novaConta);
            }

            var opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X"){
                switch (opcaoUsuario)
                {
                    
                   case "1":
                        ListarConta();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços!!!");
            Console.ReadLine();

        }

        //Deposita valor fornecido pelo usuario em uma conta também fornecida por ele
        private static void Depositar()
        {
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());
            
            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listaContas[indiceConta].Depositar(valorDeposito);
        }

        //Saca valor fornecido pelo usuario em uma conta também fornecida por ele
        private static void Sacar()
        {
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());
            
            Console.Write("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            listaContas[indiceConta].Sacar(valorSaque);
        }

        /*  Saca valor fornecido pelo usuario em sua conta e 
            Deposita valor fornecido em outra conta. 
        */
        private static void Transferir()
        {
            Console.Write("Digite o número da conta de origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
            int indiceContaDestino = int.Parse(Console.ReadLine());
            
            Console.Write("Digite o valor a ser transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            listaContas[indiceContaOrigem].Transferir(valorTransferencia,  listaContas[indiceContaDestino]);
        }

        // Lista todas as contas salvas em memoria de execução
        private static void ListarConta()
        {
            Console.WriteLine("Listar Contas");

            if(listaContas.Count == 0){
                Console.WriteLine("Nenhuma conta cadastrada");
                return;
            }

            for(int i = 0; i < listaContas.Count; i++){
                Conta conta = listaContas[i];
                Console.Write("#{0} - ", i);
                Console.WriteLine(conta);
            }
        }

        // adiciona nova conta na lista de contas ...
        private static void InserirConta()
        {
             Console.WriteLine("Inserir Conta");

             Console.Write("Digite 1 para Conta Física ou 2 para Jurídica: ");
             int entradaTipoConta = int.Parse(Console.ReadLine());

             Console.Write("Digite o Nome do Cliente: ");
             string entradaNomeCliete = Console.ReadLine();

             Console.Write("Digite o saldo inicial: ");
             double entradaSaldo = double.Parse(Console.ReadLine());

             
            Console.Write("Digite o crédito: ");
            double entradaCredito = double.Parse(Console.ReadLine());

            Conta novaConta = new Conta(
                tipoConta:(TipoConta)entradaTipoConta,
                saldo: entradaSaldo,
                credito: entradaCredito,
                nome: entradaNomeCliete
            );
            listaContas.Add(novaConta);

            
        }
    
        //Informa ao usuário quais são suas opções.
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank ao seu dispor!!!");
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1- Listar Contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();
          
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return opcaoUsuario;

        }
    }
}
