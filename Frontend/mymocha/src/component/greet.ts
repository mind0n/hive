export function sayHello(name: string) {
    return `Hello from ${name}`;
}

export function showHello(divName: string, name: string) {
    let elt = document.getElementById(divName);
    elt.innerText = sayHello(name);
}