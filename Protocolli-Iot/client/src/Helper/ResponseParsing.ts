
const son="{\"Id\":13,\"nome\":11,\"comando\":5}"
const obj = JSON.parse(son)

console.log(obj.Id)
let risp =`{"L\'id del comando è ": ${obj.Id}, "Il nome è ": ${obj.nome} , "con comando": ${obj.comando}}`;
console.log(JSON.parse(risp))