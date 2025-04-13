document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("produtoForm");
    const tabela = document.getElementById("tabelaProdutos").querySelector("tbody");

    const apiUrl = "/api/produtos";

    function carregarProdutos() {
        fetch(apiUrl)
            .then(res => res.json())
            .then(data => {
                tabela.innerHTML = "";
                data.forEach(produto => {
                    const tr = document.createElement("tr");
                    tr.innerHTML = `
                        <td>${produto.titulo}</td>
                        <td>R$ ${produto.preco.toFixed(2)}</td>
                        <td>${produto.estoque}</td>
                        <td>
                            <button onclick="editarProduto('${produto.id}')">Editar</button>
                            <button onclick="deletarProduto('${produto.id}')">Excluir</button>
                        </td>`;
                    tabela.appendChild(tr);
                });
            });
    }

    form.addEventListener("submit", (e) => {
        e.preventDefault();

        const produto = {
            id: document.getElementById("produtoId").value || undefined,
            titulo: document.getElementById("titulo").value,
            descricao: document.getElementById("descricao").value,
            preco: parseFloat(document.getElementById("preco").value),
            estoque: parseInt(document.getElementById("estoque").value),
            fotos: []
        };

        const method = produto.id ? "PUT" : "POST";
        const url = produto.id ? `${apiUrl}/${produto.id}` : apiUrl;

        fetch(url, {
            method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(produto)
        })
            .then(() => {
                form.reset();
                document.getElementById("produtoId").value = "";
                carregarProdutos();
            });
    });

    window.editarProduto = (id) => {
        fetch(`${apiUrl}/${id}`)
            .then(res => res.json())
            .then(p => {
                document.getElementById("produtoId").value = p.id;
                document.getElementById("titulo").value = p.titulo;
                document.getElementById("descricao").value = p.descricao;
                document.getElementById("preco").value = p.preco;
                document.getElementById("estoque").value = p.estoque;
            });
    };

    window.deletarProduto = (id) => {
        fetch(`${apiUrl}/${id}`, { method: "DELETE" })
            .then(() => carregarProdutos());
    };

    carregarProdutos();
});
