# Peças

Descrição das peças e mecânicas

## Clássicas

#### Rei
```
    . . . . . . .  
    . . # # # . .  
    . . # R # . .  
    . . # # # . .  
    . . . . . . . 
```

### Alcance longo

#### Rainha

#### Torre

#### Bispo

### Alcance curto

#### Cavalo
Se move em L;
```
    . . # . # . .  
    . # . . . # .   
    . . . K . . .   
    . # . . . # .   
    . . # . # . .  
```


#### Peão
Só se move pra frente e só ataca na diagonal (pra frente). No primeiro movimento pode andar 2 casas.
```
    . . . .   . . # .
    . P + .   . P . .
    . . . .   . . # .
```


## Novas

### Alcance longo

#### Arqueiro
Só se move para frente, mas captura em qualquer diagonal para frente.
Ao chegar ao fim do tabuleiro vira bispo.

```
    . . . . . # .
    . . . . # . .
    . . . A + . .
    . . . . # . .
    . . . . . # .
```

### Alcance curto

#### Lanceiro 
Só anda e ataca pra frente, como o peão. Pode se mover 1 casa para frente normalmente, mas ataca até 2 casas para frente, pode inclusive "pular" peças, semelhante ao cavalo.
```
    . . . .   . . . . .
    . L + .   . L # # .
    . . . .   . . . . .
```
Ao chegar ao fim do tabuleiro pode evoluir para torre ou cavalo.

#### Elefante

Pode andar para qualquer peça num distância de manhatan de até 2 blocos (não pula obstáculos).

```
    . . . # . . . .  
    . . # # # .  . .  
    . # # E # # . .  
    . . # # # . . .  
    . . . # . . . .  
```

#### Príncipe
Se move como o rei.
```
    . . . . . . .  
    . . # # # . .  
    . . # P # . .  
    . . # # # . .  
    . . . . . . . 
```
Ao chegar ao fim do tabuleiro pode virar bispo, torre, cavalo, rainha ou rei:
- Se o principe virar rei, o rei pode se tornar bispo, torre ou cavalo.
- O principe só pode virar rei se não entrar em cheque ao fazer isso (mas o rei original poderia estar em cheque?).

#### Escudo

Ao chegar ao fim do tabuleiro pode virar torre.
```
    . . . . .
    . . # . .
    . S # . .
    . . # . .
    . . . . .
```

<br><br>

Tabuleiro
```
    . . . . . . .
    . . . . . . .
    . . . . . . .
    . . . . . . .
    . . . . . . .
```