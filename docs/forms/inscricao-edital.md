# Formulário de Inscrição (Edital PIBIC/PIBIC-EM)

## Campos a serem preenchidos

**TipoPrograma**
  - PIBIC (Graduação)
  - PIBIC-EM (Ensino Médio)

**Orientador**
  - Nome (String)
  - CPF (Numeric)
  - Matrícula SIAPE (String)
  - E-mail Instituicional
  - Identificar Lattes (Numeric)

**DadosProjeto**
  - Título do Projeto de Iniciação Científica
  - Grande Área (areas.txt -> *.00.00.00)
  - Área (areas.txt -> *.00.00)
  - Sub-área (areas.txt -> others)
  - É candidato a Bolsa? (Boolean)
  - Palavras Chave (3x - String)

**DadosAluno**
  - Nome (String)
  - CPF (Numeric)
  - Data de Nascimento (Date)
  - Identidade (Numeric)
  - Órgão Emissor (String)
  - Data Expedição (Date)
  - Sexo (Select -> String)
    - Masculino
    - Feminino
  - Cor/Raça (Select -> String)
    - Amarela
    - Branca
    - Indígena
    - Não declarado
    - Não dispõe da informação
    - Parda
    - Preta
  - Endereço Residencial (Rua, Avenida, N.º, complemento, bairro -> String)
  - Cidade (String)
  - UF (Select -> String)
  - CEP (Numeric)
  - DDD Telefone (Numeric - Optional)
  - Telefone (Numeric - Optional)
  - DDD Celular (Numeric - Optional)
  - Celular (Numeric - Optional)
  - E-mail Institucional (String)
  - Unidade (Select -> String)
    - Maracanã
    - Angra dos Reis
    - Itaguaí
    - Maria da Graça
    - Nova Iguaçu
    - Nova Friburgo
    - Valença
    - Petrópolis
  - Curso (Select -> cursos.txt)
  - Ano de entrada / Semestre
  - O aluno possui bolsa de assistência estudantil
    - Programa de Auxílio ao Estudante com Deficiência (PAED)
    - Programa de Auxílio Emergencial (PAEm)
    - Programa de Auxílio ao Estudante (PAE)
    - Auxílio Digital
    - Outra
    - Não possui bolsa de assistência estudantil

**PropostaProjetoIC**
  - Objetivos (Até 1500 caracteres com espaço -> String)
  - Metodologia (Até 1500 caracteres com espaço -> String)
  - Resultados Esperados (Até 1500 caracteres com espaço -> String)

**CronogramaExecucao**
  - Atividades / Período (String)

**PontuacaoAtividades** (Índice AP - Edital PIBIC)
  - Pegar do arquivo Excel <--

**CompromissoOrientador**
  - Declaro que li e concordo com as normas do presente Edital. (Boolean)
  - Declaro para fins de direito, conhecer as normas fixadas pelo programa pibic para a concessão de bolsa de iniciação científica e assumo o compromisso de dedicar-me às atividades de orientação durante a vigência do benefício. (Boolean)