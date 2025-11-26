


Ideias de arquiteturas pra construção do jogo:

-> Manager Global :: monobehavior
    controla os turnos e o jogo como um todo

-> Board :: monobehavior
    mantém a lista de peças e é responsável por renderizar as coisas no tabuleiro

-> Cell 
    responsável por renderizar o quadrado e armazenar outras informações que possam ter relação com as habilidades de peças, etc?

-> Piece :: MonoBehavior 
    guarda informações básicas, como posição no tabuleiro, cor, responsável por renderizar as peças e bla bla bla

    -> King :: Piece
        guarda informações do tipo de peça e funções como casas disponíveis para andar e para atacar. Podem acionar eventos em board?
    -> Queen
    -> ....


