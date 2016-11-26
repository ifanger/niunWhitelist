# niunWhitelist
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=C67HPRXBWNM7Q&lc=BR&item_name=Niunzin&item_number=DONATE_NIUN&currency_code=BRL&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted)
### Introdução
niunWhitelist (ou simplesmente nWL) é um projeto gratuito e de código aberto composto por dois softwares, que tem como objetivo fornecer facilidade na hora de adicionar jogadores na whitelist do seu servidor. A instalação do mesmo pode não ser tão simples, pois varia de servidor para servidor, mas vou tentar lhe explicar da maiera mais simples.
O mesmo já realiza conversão de UID para GUID no próprio client, tornando a tarefa muuuito simples.

### Como funciona?
Existem dois aplicativos: o cliente e o servidor. O servidor é de distribuição única no servidor, ele deverá estar na mesma máquina onde seu servidor o Arma 3 está rodando, pois ele precisa ter acesso a dois arquivos essenciais: o de whitelist e o de reiniciar o BEC.
Já o cliente, você pode distribuir entre os administradores do seu servidor, mas tenha cuidado, pois a senha do servidor fica armazenado num arquivo de texto, portanto, se alguém agir de má fé e divulgar o mesmo, você deverá trocar a senha do seu servidor nWL imediatamente. (caso contrário, pessoas não autorizadas conseguirão adicionar jogadores na whitelist)
O cliente conecta-se com o servidor e faz uma requisição de entrada contendo a senha do servidor. O servidor analisa se a mesma é verdadeira, e se for, da acesso ao usuário. Após o acesso, o usuário consegue fazer a adição dos jogadores com apenas um click. O cliente enviará pacotes para o servidor, e o mesmo apenas irá acrescentar as informações ao arquivo de texto e reiniciar o BEC, para que as mudanças tenham efeito.

## Servidor
### Requisitos
  - Battleye Extended Controls (BEC)
  - .NET Framework 4.0 (ou superior)
  - Sistema de whitelist ativa no servidor
  - Arquivo BAT para reiniciar o mesmo

**Nota:** compatível apenas com Windows, em breve será desenvolvida uma versão em Python para múltiplas plataformas.

### Configuração
Primeiramente você deverá abrir o arquivo ```nwl_config.txt```.
O arquivo possui detalhes de como configurar cada opção, o que você precisa saber no momento é que ```key``` é a chave de segurança (senha) do servidor, se alguém tiver acesso à essa senha, conseguirá adicionar jogadores na whitelist.

Após toda a configuração, basta executar o ```Server.exe``` e deixar o mesmo ligado.

## Cliente
### Requisitos
  - Sistema operacional Windows
  - .NET Framework 4.0 (ou superior)
  - Conexão com a internet
 
### Configuração
Novamente, trata-se do arquivo ```nwl_config.txt```, que é autoexplicativo.
Você apenas precisa saber ao IP no qual o servidor está rodando, a porta e a senha do mesmo.
**Nota:** Servidor é o servidor do nWL, e não do Arma3.

Após a configuração do mesmo, você já pode distribuir o mesmo para seus administradores.
MAS TENHA CUIDADO! Se cair em mãos erradas, qualquer um poderá editar a senha, e consequentemente, você deverá imediatamente trocar a senha do seu servidor nWL.

## Downloads
Se você não possui condições de compilar o aplicativo, faça o download da última versão estável:

[Cliente](https://github.com/Niunzin/niunWhitelist/tree/master/Downloads/Client) | [Servidor](https://github.com/Niunzin/niunWhitelist/tree/master/Downloads/Server) | [Conversor](https://github.com/Niunzin/niunWhitelist/raw/master/Tools/Conversor.rar)
