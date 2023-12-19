// npm run cypress:open
describe('Switch between LightMode and DarkMode', () => {
    it('should switch between LightMode and DarkMode', () => {
        cy.visit('https://localhost:5173');
        cy.get('.DarkMode').should('exist');
        cy.get('[data-cy=backgroundButton]').click();
        cy.get('.LightMode').should('exist');
        cy.get('[data-cy=backgroundButton]').click();
        cy.get('.DarkMode').should('exist');
    });
});