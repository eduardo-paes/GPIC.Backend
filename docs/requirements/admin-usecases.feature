# Casos de uso do administrador

# TODO:
  # - O administrador terá acesso ao módulo de controle de acesso da plataforma;
  # - No módulo de acesso será possível controlar Perfis, Permissões e Usuários;
  # - Ele poderá criar usuários do tipo Analista;
  # - Usuários do tipo analista podem ser usuários novos ou professores com conta ativa;
  # - Ao criar uma conta nova do tipo analista, um e-mail é enviado para o e-mail cadastrado na nova conta solicitando que a pessoa conclua seu cadastro;
  # - Caso o administrador deseje tornar um professor um analista, basta alterar o perfil do professor para analista;
  # - (?) Todo analista possui as mesmas permissões de professor;
  # - (?) Todo administrador possui as mesmas permissões de analista;
  # - CRUD GrandeArea, Area, SubArea, TipoPrograma, Edital.

  # Mapping: [User <> UserRole <> Role <> RolePermission <> Permission]
  # Mapping Roles: [Student, Professor, Approver, Admin]
  # Mapping Actions: [Read, Create, Update, Delete]