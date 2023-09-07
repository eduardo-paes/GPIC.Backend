#!/bin/bash

# Gere uma API Key aleatória
api_key=$(openssl rand -hex 32)

# Defina a API Key como uma variável de ambiente
export SEQ_API_KEY=$api_key

# Inicie o Seq Logger
exec "$@"
