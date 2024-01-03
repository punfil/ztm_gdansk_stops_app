// npm run cypress:open

describe('Stops operations', () => {
    before(() => {
        cy.visit('https://localhost:5173');
        cy.get('[data-cy=loginn]').click();
        cy.get('[data-cy=username]').type('wpanfil');
        cy.get('[data-cy=password]').type('1234');
        cy.get('[data-cy=submit]').click();
        cy.get('[data-cy=logoutt]').should('exist');
    });

    it('Should add stop to favourites', () => {
        // Mock server response for successful adding to favorites
        cy.intercept('POST', '**/addstop*', {
            statusCode: 200,
            body: {
                success: true,
            },
        }).as('addToFavoritesRequest');

        cy.get('[data-cy=stopss]').click();
        cy.wait(3000);
        cy.get('[data-cy=addtofav]').first().click();
        cy.get('[data-cy=userfavs]').first().click();
        cy.get('[data-cy=removefav]').should('exist');
    });
});
