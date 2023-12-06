import { shallowMount } from '@vue/test-utils';
import Dummyth from '@/components/Dummyth.vue';
import DummyInput from '@/components/DummyInput.vue';

describe('Dummyth', () => {
    it('renders the prop value in the <th> element', () => {
        const parentMessage = 'Test Message';

        const wrapper = shallowMount(Dummyth, {
            propsData: {
                ParentMessage: parentMessage,
            },
        });

        expect(wrapper.find('th').text()).toBe(parentMessage);
    });
});

describe('Dummyth', () => {
    it('check if the prop value is rendered in the template', () => {
        const parentMessage = 'Test Message';

        const wrapper = shallowMount(Dummyth, {
            propsData: {
                ParentMessage: parentMessage,
            },
        });

        expect(wrapper.text()).toContain(parentMessage);
    });
});

describe('DummyInput', () => {
    it('emits update:modelValue event on input change', async () => {
        const modelValue = 'initialValue';

        const wrapper = shallowMount(DummyInput, {
            props: {
                modelValue,
            },
        });

        const inputElement = wrapper.find('input');

        await inputElement.setValue('newValue');

        expect(wrapper.emitted('update:modelValue')).toBeTruthy();
        expect(wrapper.emitted('update:modelValue')[0]).toEqual(['newValue']);
    });
});
