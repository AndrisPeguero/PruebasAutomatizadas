import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.support.ui import WebDriverWait, Select
from selenium.webdriver.support import expected_conditions as EC
from webdriver_manager.chrome import ChromeDriverManager
from datetime import datetime
import os
import time

os.makedirs("capturas", exist_ok=True)
imagenes = []

@pytest.fixture
def navegador():
    servicio = Service(ChromeDriverManager().install())
    driver = webdriver.Chrome(service=servicio)
    driver.maximize_window()
    yield driver
    driver.quit()

def esperar(segundos):
    time.sleep(segundos)

def guardar_captura(driver, nombre):
    fecha = datetime.now().strftime("%Y%m%d_%H%M%S")
    ruta = f"capturas/{nombre}_{fecha}.png"
    driver.save_screenshot(ruta)
    print(f"Captura tomada: {ruta}")
    imagenes.append((nombre, ruta))

def crear_reporte(resultado_final):
    color = "#d4edda" if resultado_final == "SUCCESS" else "#f8d7da"
    texto_color = "#155724" if resultado_final == "SUCCESS" else "#721c24"
    html = f"""
    <html>
    <head>
        <title>Reporte de Pruebas</title>
        <style>
            .resultado {{
                background-color: {color};
                color: {texto_color};
                padding: 15px;
                border-radius: 10px;
                font-weight: bold;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <h1>Pruebas Selenium - Sistema Local</h1>
        <p>Fecha: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}</p>
        <div class='resultado'>Resultado Final: {resultado_final}</div>
    """
    for titulo, ruta in imagenes:
        html += f"<h2>{titulo}</h2><img src='{ruta}' width='700'><br><br>"

    html += "</body></html>"
    with open("reporte.html", "w", encoding="utf-8") as archivo:
        archivo.write(html)
    print("Reporte generado: reporte.html")

def test_login_y_registro(navegador):
    resultado_final = "SUCCESS"
    try:
        navegador.get("http://localhost:8080/login.html")
        esperar(2)
        guardar_captura(navegador, "01_Login")

        navegador.find_element(By.NAME, "usuario").send_keys("Andris")
        navegador.find_element(By.NAME, "contraseña").send_keys("12345654")
        navegador.find_element(By.CSS_SELECTOR, "input[type='submit']").click()
        esperar(5)
        guardar_captura(navegador, "02_Login_incorrecto")
        
        
        navegador.find_element(By.NAME, "usuario").clear()
        navegador.find_element(By.NAME, "contraseña").clear()
        navegador.find_element(By.NAME, "usuario").send_keys("admin")
        navegador.find_element(By.NAME, "contraseña").send_keys("1234")
        navegador.find_element(By.CSS_SELECTOR, "input[type='submit']").click()
        esperar(15)
        guardar_captura(navegador, "03_Login_correcto")
        
       
        
        navegador.get("http://localhost:8080/front.html")
        esperar(2)
        guardar_captura(navegador, "04_Formulario_Registro")

        navegador.find_element(By.ID, "nombre").send_keys("UsuarioTest")
        navegador.find_element(By.ID, "correo").send_keys("test@example.com")
        navegador.find_element(By.ID, "clave").send_keys("1234")
        
        esperar(2)
        guardar_captura(navegador, "05_Registro_Completado")
        select_status = Select(navegador.find_element(By.ID, "status"))
        select_status.select_by_visible_text("Activo")

        navegador.find_element(By.CSS_SELECTOR, "button[type='submit']").click()

        WebDriverWait(navegador, 10).until(
            EC.presence_of_element_located((By.ID, "data"))
        )        
        esperar(5)
        guardar_captura(navegador, "06_Lista_Usuarios")

    except Exception as e:
        print("Error:", e)
        guardar_captura(navegador, "ERROR")
        resultado_final = "FAIL"
        raise AssertionError("Prueba fallida: " + str(e))
    finally:
        crear_reporte(resultado_final)

if __name__ == "__main__":
    pytest.main([__file__])
