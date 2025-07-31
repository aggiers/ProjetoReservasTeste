class RegistroForm {
  elements ={
    titleInput: () => cy.get('#title'),
    titleFeedback: () => cy.get('#titleFeedback'),
    imageUrlInput: () => cy.get('#imageUrl'),
    imageUrlInputFeedback: () => cy.get('#urlFeedback'),
    submitBtn: () => cy.get('#btnSubmit'),
  }

  clickSubmit(){
    this.elements.submitBtn().click()
  }

  typeTitle(text){
    if(!text) return;
    this.elements.titleInput().type(text)
  }

  typeUrl(url){
    if(!url) return;
    this.elements.imageUrlInput().type(url)
  }
}

const registroForm = new RegistroForm();

describe('Registro de imagem', () => {
  describe('Enviar uma imagem com entradas inválidas', () => {
    const imagem = {
      titulo:'',
      url: ''
    }

    it('Estou na página de cadastro de imagens', () => {
      cy.visit('/')
    })

    it(`Quando eu digito "${imagem.titulo}" no campo do título`, () => {
      registroForm.typeTitle(imagem.titulo)
    })
    it(`Quando eu digito "${imagem.url}" no campo de URL`, () => {
      registroForm.typeUrl(imagem.url)
    })

    it('Eu clico no botão de envio', () => {
      registroForm.clickSubmit()
    })
    it('Então eu devo ver a mensagem "Please type a tittle for the image" abaixo do campo de título', () => {
      registroForm.elements.titleFeedback().should("contains.text", "Please type a title for the image")
    })
    it('E eu devo ver a mensagem "Please type a valid URL" abaixo do campo de URL da imagem', () => {
      registroForm.elements.imageUrlInputFeedback().should("contains.text", "Please type a valid URL")
    })
  })

  describe('Enviar uma imagem com entradas válidas', () => {
    const imagem = {
      titulo:'banana',
      url: 'https://www.hortifrutiorganico.com.br/121-large_default/banana-organica-prata-1-kg.jpg'
    }

    it('Estou na página de cadastro de imagens', () => {
      cy.visit('/')
    })

    it(`Quando eu digito "${imagem.titulo}" no campo do título`, () => {
      registroForm.typeTitle(imagem.titulo)
    })
    it(`Quando eu digito "${imagem.url}" no campo de URL`, () => {
      registroForm.typeUrl(imagem.url)
    })
     it('Eu clico no botão de envio', () => {
      registroForm.clickSubmit()
    })

  })
})
