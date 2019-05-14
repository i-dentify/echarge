import makeRoute from 'lib/routing/generator';
import layout from 'components/layout';
import list from './list';
import add from './add';
import details from './details';
import edit from './edit';
import remove from './delete';

const routes = [
    makeRoute('/cars', layout, list),
    makeRoute('/cars/add', layout, add),
    makeRoute('/cars/:id', layout, details),
    makeRoute('/cars/:id/edit', layout, edit),
    makeRoute('/cars/:id/delete', layout, remove),
];

export default routes;
