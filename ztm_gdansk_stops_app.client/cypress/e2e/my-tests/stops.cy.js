// npm run cypress:open
describe('Stops', () => {
    describe('Verify showing details of a stop', () => {
        it('should show stop details when button is clicked', () => {
            cy.visit('https://localhost:5173');
            cy.get('[data-cy=stopss]').click();
            cy.wait(3000);
            cy.get('[data-cy=showStopDetails]').first().click();
            cy.get('.styled-table').should('exist');
        });
    });
});

