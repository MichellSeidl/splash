// Função para abrir o modal ao clicar no botão "AÇÕES"
function openModal() {
    var modal = document.getElementById('myModal');
    modal.style.display = 'block';
}

// Função para fechar o modal
function closeModal() {
    var modal = document.getElementById('myModal');
    modal.style.display = 'none';
}

// Adicionando evento de clique ao botão "AÇÕES"
var acoesBtn = document.querySelector('.acoesFrame');
acoesBtn.addEventListener('click', openModal);

// Adicionando evento de clique ao botão de fechar no modal
var closeBtn = document.querySelector('.close');
closeBtn.addEventListener('click', closeModal);

// Adicionando evento de clique aos botões "Enviar", "Fechar" e "Limpar" dentro do modal
document.getElementById('submitBtn').addEventListener('click', submitDate);
document.getElementById('closeModalBtn').addEventListener('click', closeModal);
document.getElementById('resetDateBtn').addEventListener('click', resetDate);

// Função para enviar a data inserida
function submitDate() {
    var dateInput = document.getElementById('dateInput').value;
    console.log('Data inserida:', dateInput);
}

// Função para limpar o campo de data
function resetDate() {
    document.getElementById('dateInput').value = '';
}
