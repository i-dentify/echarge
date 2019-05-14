import makeRoute from 'lib/routing/generator';
import layout from 'components/layout';
import list from './list';
import add from './add';
import details from './details';
import edit from './edit';
import remove from './delete';
import charges from './charges/list';
import acceptInvitation from './invitations/accept';

const routes = [
    makeRoute('/locations', layout, list),
    makeRoute('/locations/add', layout, add),
    makeRoute('/locations/:id', layout, details),
    makeRoute('/locations/:id/edit', layout, edit),
    makeRoute('/locations/:id/delete', layout, remove),
    makeRoute('/locations/:id/charges/:user', layout, charges),
    makeRoute('/locations/invitations/accept', layout, acceptInvitation),
];

export default routes;
