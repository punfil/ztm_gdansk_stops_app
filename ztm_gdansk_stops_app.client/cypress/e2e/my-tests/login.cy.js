// npm run cypress:open
describe('Login', () => {
    it('Handles successful login', () => {
        cy.intercept('POST', '**/login/*', {
            statusCode: 200,
            body: {
                userId: 1,
                username: 'validUser',
                password: 'validPassword'
            },
        }).as('loginRequest');
        cy.visit('https://localhost:5173');
        cy.get('[data-cy=loginn]').click();
        cy.get('[data-cy=username]').type('validUser');
        cy.get('[data-cy=password]').type('validPassword');
        cy.get('[data-cy=submit]').click();
        cy.wait('@loginRequest');
        cy.get('[data-cy=logoutt]').should('exist');
    });

    it('Handles failed login', () => {
        cy.intercept('POST', '**/login/*', {
            statusCode: 401,
            body: {
                error: 'Invalid credentials',
            },
        }).as('loginRequest');
        cy.visit('https://localhost:5173');
        cy.get('[data-cy=loginn]').click();
        cy.get('[data-cy=username]').type('invalidUser');
        cy.get('[data-cy=password]').type('invalidPassword');
        cy.get('[data-cy=submit]').click();
        cy.wait('@loginRequest');
        cy.get('@loginRequest').then(() => {
            cy.get('[data-cy=msg]').should('have.text', 'Failed to login invalidUser');
        });
    });
});