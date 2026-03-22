const formRegistro = document.getElementById("form-registro");
const inputName = document.getElementById("Name");
const inputLastName = document.getElementById("LastName");
const inputEmail = document.getElementById("email");
const inputUserName = document.getElementById("UserName");
const inputpassword = document.getElementById("Password");
const inputTel = document.getElementById("tel");
const inputImg = document.getElementById("imagePerfil");
const inputRol = document.getElementById("rol"); 
const inputRnc = document.getElementById("RNC");
const containerRNC = document.getElementById("field-RNC");
const errorRNC = document.getElementById("error-rnc");
const btnLimpiar = document.getElementById("btn-login-resset");


//errores del form
const nameError = document.getElementById("error-name");
const lastNameError = document.getElementById("error-LastName");
const emailError = document.getElementById("error-email");
const userNameError = document.getElementById("error-UserName");
const passwordError = document.getElementById("error-password");
const telError = document.getElementById("error-tel");
const imageError = document.getElementById("error-image");
const errorRol = document.getElementById("error-role");
const errorNotEspecifico = document.getElementById("error-no-especifico");






formRegistro.addEventListener("submit",function(e){
e.preventDefault();


    if(!validateInputRegistro()){
        return;
    }

    const img = inputImg.files[0];
    RegistroEnpoint(
        inputName.value,
        inputLastName.value,
        inputEmail.value,
        inputUserName.value,
        inputpassword.value,
        inputTel.value,
        img,
        inputRol.value,
        inputRnc.value
    );

});


btnLimpiar.addEventListener("click",function(e){
  clearInput();
});





function validateInputRegistro()
{
   isValid = true;
   
   isValid = validateInput(inputName,isValid);
   isValid = validateInput(inputLastName,isValid);
   isValid = validateInput(inputEmail,isValid);
   isValid = validateInput(inputpassword,isValid);
   isValid = validateInput(inputTel,isValid);
   isValid = validateInput(inputRol,isValid);
   isValid = validateInput(inputImg,isValid);
   isValid = validateInput(inputUserName,isValid);

   
   if(inputRol.value === "Propietario"){
    isValid = validateInput(inputRnc,isValid);
    }


   if(!isValid){
      alert("debes llenar todos los campos");
   }

   return isValid;
      
}




function validateInput(input,isValid){

let empty;

if(input.type === "file"){
    empty = input.files.length === 0;
}else{
    empty = input.value === undefined || input.value.trim() === "";
}

 if(empty){
    input.classList.add("input-error");
    input.classList.remove("input-success");
    messageError(input.id);
    return false;
 }else{
    input.classList.remove("input-error");
    input.classList.add("input-success");
    borrarMessage(input.id);
    return isValid;
 }  

}




function messageError(id){

  switch(id)
  {
      case "Name":
           nameError.innerText = "debes introduccir un nombre";
        break; 
     case "LastName":
           lastNameError.innerText = "debes introduccir un apellido";
        break;
     case "email": 
           emailError.innerText = "debes introduccir un correo";
         break;
      case "UserName":
            userNameError.innerText = "debes introduccir un nombre de usuario";
           break;   
      case "Password":
             passwordError.innerText = "debes introduccir una contraseña";
          break;
       case "tel":
            telError.innerText = "debes indicar un numero de telefono";
         break;
       case "imagePerfil":
           imageError.innerText = "debes indicar una imagen";
            break; 
       case "rol":
             errorRol.innerText = "debes indicar un rol";
            break;
        case "RNC":
            errorRNC.innerText = "debes indicar un rnc";
            break;
  }

}


function borrarMessage(id){

switch(id)
{
case "Name":
nameError.innerText = "";
break;

case "LastName":
lastNameError.innerText = "";
break;

case "email":
emailError.innerText = "";
break;

case "UserName":
userNameError.innerText = "";
break;

case "Password":
passwordError.innerText = "";
break;

case "tel":
telError.innerText = "";
break;

case "imagePerfil":
imageError.innerText = "";
break;

case "rol":
errorRol.innerText = "";
break;

case "RNC":
errorRNC.innerText = "";
break;
}

}





function clearInput(){
  
    inputs = [inputName,inputLastName,inputUserName,inputEmail,inputRol,inputpassword,inputTel,inputImg];

    for(let i = 0; i < inputs.length; i++){
        inputs[i].value = "";
        inputs[i].classList.remove("input-error");
        inputs[i].classList.remove("input-success");
    }

    inputName.focus();

}




function validatePassword(password, input) {

let isValid = true;
let message = "";

if(password.length < 8 || password.lenght > 8){

passwordError.innerText = "La contraseña debe tener  8 caracteres";
isValid = false;

}

else if(!/[0-9]/.test(password)){

passwordError.innerText = "La contraseña debe tener al menos un número";
isValid = false;

}

else if(!/[!@#$%^&*(),.?":{}|<>]/.test(password)){

passwordError.innerHTML = "La contraseña debe tener al menos un carácter especial";
isValid = false;

}

else if(!/[A-Z]/.test(password)){

passwordError.innerText = "La contraseña debe tener una mayúscula";
isValid = false;

}

else if(!/[a-z]/.test(password)){

passwordError.innerText = "La contraseña debe tener una minúscula";
isValid = false;

}

if(!isValid){

input.classList.add("error-input");
input.classList.remove("error-success");

}else{

input.classList.remove("error-input");
input.classList.add("error-success");

}

return isValid;

}


function validateDominio(email){

   const dominios = ["@gmail.com","@outlook.com","@hotmail.com","@yahoo.com","@icloud.com","@aol.com"];

   for(let i = 0; i < dominios.length; i++){
      if(email.includes(dominios[i])){
         return true;
          
      }
   }
   
   return false;
}

function validateTel(tel){

const regex = /^[0-9]{10}$/;

if(!regex.test(tel)){
    telError.innerText = "El numero de telefono debe ser valido y tener 10 digitos";
    return false;
}

telError.innerText = "";
return true;

}



function validateEmail(email)
{
    if(!validateDominio(email)){
        emailError.innerText = "Debes introduccir un correo valido, revisa el dominio";
        return false;
    }

    return true;
}



inputRol.addEventListener("change",function(){

    if(inputRol.value === "Propietario"){
         containerRNC.style.display = "block"
    }else{
      
         containerRNC.style.display = "none"

    }

})



async function RegistroEnpoint(Name,lastName,email,userName,Password,Phone,Image,Rol,Rnc)
{
    try{
          
       const _email = validateEmail(email);
       const _password = validatePassword(Password,inputpassword);
       const _phone = validateTel(Phone);

    
       if(!_email || !_password || !_phone){
           return ;   
       }

           const formData = new FormData(); 
           formData.append("Name",Name);
           formData.append("LastName",lastName);
           formData.append("Email",email);
           formData.append("UserName",userName);
           formData.append("Password",Password);
           formData.append("Phone",Phone);
           formData.append("ProfileImage",Image);
           formData.append("Role",Rol);
           formData.append("RNC",Rnc)
        
           
      
       let res = await  axios.post("https://localhost:7039/Api/v1/LoginUser/register",formData),
       Json = await res.data;
           
       errorNotEspecifico.innerText = Json.errors;

       if(Json.errors.length > 0){
          return;
       }
       
       localStorage.setItem("message",Json.message);
       window.location.href = "/Assets/view/Login.html";

    }catch(err){
     console.log("ERROR COMPLETO:", err);
      if(err.response){
       errorNotEspecifico.innerText = `Error ${err.response.statusText}`;

    }else{
         errorNotEspecifico.innerText = "No se pudo conectar con el servidor";
}

}
}



