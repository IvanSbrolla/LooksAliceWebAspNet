//Home - Index;
//Produtos;
function AlterarCorDeMostruario(obj, CaminhoNovaImagem) {
    document.getElementById(obj).src = CaminhoNovaImagem;
}
function MostrarAddC(obj) {
    document.getElementById(obj).style.opacity = "2";
    document.getElementById(obj).style.zIndex = "2";
    document.getElementById('boxAdicionarAoCarrinho').style.zIndex = '5';
}

function EsconderAddC(obj) {
    document.getElementById(obj).style.opacity = "0";
    document.getElementById(obj).style.zIndex = "-1";

};






