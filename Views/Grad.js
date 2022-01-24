export function preuzmiGradove(dropDown)
{
    console.log( $(".drzaveDropdown option:selected").val());
    fetch("https://localhost:5001/Grad/PreuzmiGradove/" + $(".drzaveDropdown option:selected").val(),
    {method : 'GET'}
    )
    .then(p => {
        p.json().then(grad => {
            dropDown.innerHTML = "";

            grad.forEach(infoGrad => {
                var option = document.createElement('option');
                option.text = infoGrad.naziv;
                option.value = infoGrad.id;
                dropDown.add(option);
            });
        })
    })
}


export function createGradDropdown(container)
{
    var containerGrad = document.createElement("div");
    containerGrad.classList.add("grad-select","col-12");

    var selectDrzava = document.createElement("select");
    selectDrzava.name = "gradovi";
    selectDrzava.classList.add("gradoviDropdown",  "h-100", "w-100");
    containerGrad.appendChild(selectDrzava);
    preuzmiGradove(selectDrzava);

    container.appendChild(containerGrad);
}