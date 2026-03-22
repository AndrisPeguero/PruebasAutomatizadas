
const formResset = document.getElementById("resset-password");
const inputUserName = document.getElementById("userName");
const errorUserName = document.getElementById("error-userName");
const messageErrorResset = document.getElementById("message-error-noEspecifico");
const formChangePassword = document.getElementById("change-password-form");



formResset.addEventListener("submit",function(e){
    e.preventDefault();
    
    if(!validateInput(inputUserName)){
        return;
    }
   
    RessetPasswordEnpoint(inputUserName.value);
    
})





function validateInput(input){

    if(input.value == null || input.value == undefined || input.value === ""){
        
        input.classList.add("input-error");
        input.classList.remove("input-success");
        errorUserName.innerText = "debes indicar un nombre de usuario";
        return false;
    }else{

        input.classList.remove("input-error");
        input.classList.add("input-success");
        errorUserName.innerText = "";
        return true;
    }
      
}




async function RessetPasswordEnpoint(userName){

   try{

      let response = await axios.post("https://localhost:7039/Api/v1/LoginUser/get-resset-token",{
         userName
      }),
        
    json = await response.data;
    
    if(json.errors && json.errors.length > 0 && json.hasError){
        messageErrorResset.innerText = json.errors;
        return;
    }

    localStorage.setItem("resset-success","Please Check your email, for resset your password");
    window.location.href = "/Assets/view/Login.html";
   }catch(err){
     let message = err.response.statusText || "Ocurrio un error con el servidor";
     messageErrorResset.innerText = `Error ${message}`;
     return;
   }

}

