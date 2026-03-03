
// On attend 5 secondes (5000 ms) puis on fait disparaître l'alerte
setTimeout(function () {
    var alert = document.getElementById('success-alert');
    if (alert) {
        alert.style.transition = "opacity 0.5s ease-out";
        alert.style.opacity = "0";
        setTimeout(function () { alert.remove(); }, 500); // supprime l'élément après la transition
    }
}, 5000);
