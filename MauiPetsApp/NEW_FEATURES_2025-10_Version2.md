# Novas Funcionalidades – MauiPets

Este documento resume as principais funcionalidades adicionadas recentemente (Out/2025).

---

## 📸 Galeria de Fotos do Pet (`PetGalleryPage`)

- **Gestão de Galeria por Animal**
  - Cada pet tem agora uma galeria de fotos associada.
  - É possível:
    - Adicionar fotos (usando a câmara ou galeria do dispositivo).
    - Visualizar fotos em modo de galeria/carrossel.
    - Eliminar fotos individuais.
    - Ampliar/visualizar foto em popup.
  - As fotos são guardadas localmente na app e associadas ao animal.

- **Integração UI/UX**
  - Acesso à galeria diretamente a partir do perfil do animal.
  - Confirmação visual e mensagens toast para feedback de ações (ex: remoção de foto).
  - Fácil navegação entre galeria e detalhes do pet.

---

## 🔐 Backup e Restauração de Dados (`BackupPage`)

- **Backup Manual**
  - Possibilidade de criar backups da base de dados local da aplicação via interface.
  - O utilizador pode visualizar o nome, data e localização do último backup.
  - O backup é guardado em ficheiro local, com indicação visual de sucesso/erro.
  - Proteção contra sobreposição não-intencional: confirmação antes de substituir backups existentes.

- **Restauração Segura**
  - Permite restaurar a base de dados local a partir de um backup existente.
  - Confirmação obrigatória antes de substituir os dados atuais.
  - Informação visual sobre alterações entre o estado corrente e o backup.
  - Processo de restore com feedback ao utilizador e indicação de sucesso ou falha.

---

## 📄 Exportação e Partilha de Ficha PDF (`PetFichaPdfService`)

- **Geração de PDF com Ficha Completa do Animal**
  - Criação de um ficheiro PDF detalhado para cada pet, contendo:
    - Dados principais (nome, espécie, raça, idade, chip, etc.)
    - Historial de vacinas, desparasitantes, rações e consultas veterinárias.
    - Formatação consistente de datas e campos.
    - Diagnóstico e tratamento de consultas apresentados com wrap e em linhas separadas.
    - Se possível, inclusão da(s) foto(s) do animal.
  - Idade calculada automaticamente a partir da data de nascimento.

- **Partilha Simplificada**
  - O PDF pode ser partilhado diretamente através das opções nativas do dispositivo (e-mail, WhatsApp, etc.).
  - Possibilidade de guardar o PDF localmente para posterior utilização.

---

## Segurança e Privacidade

- **Validação e Confirmação em Ações Críticas**
  - Ações de backup/restauração e eliminação de fotos requerem confirmação do utilizador.
  - Mensagens claras e feedback visual em todas as operações sensíveis.

- **Gestão Local dos Dados**
  - Fotos e ficheiros de backup são geridos localmente, respeitando a privacidade do utilizador.
  - Não há envio de dados sensíveis para servidores externos sem ação do utilizador.

---

*Para mais detalhes sobre cada funcionalidade, consulte a documentação técnica ou explore a interface da aplicação.*
