import React from 'react';
import type { RuleDto, RuleError } from '../types';

interface RuleInputProps {
    rule: RuleDto;
    onChange: (updatedRule: RuleDto) => void;
    onRemove: () => void;
    isRemovable: boolean;
    error?: RuleError;
}

const RuleInput: React.FC<RuleInputProps> = ({ rule, onChange, onRemove, isRemovable, error }) => {

    const handleDivisibleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({ ...rule, divisibleBy: parseInt(e.target.value, 10) });
    };

    const handleWordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        onChange({ ...rule, word: e.target.value });
    };

    return (
        <div className="flex items-center gap-4 mb-2">
            <div className="flex-1">
                <label className="block text-sm font-medium">Divisible By</label>
                <input
                    type="number"
                    value={rule.divisibleBy}
                    onChange={handleDivisibleChange}
                    className="w-full border px-3 py-2 rounded"
                    placeholder='e.g., 3'
                    min={1}
                />
                {error?.divisibleBy && <span className="text-red-500 text-sm">{error.divisibleBy}</span>}
            </div>
            <div className="flex-1">
                <label className="block text-sm font-medium">Word</label>
                <input
                    type="text"
                    value={rule.word}
                    onChange={handleWordChange}
                    className="w-full border px-3 py-2 rounded"
                    placeholder='e.g., "Fizz"'
                />
                {error?.word && <span className="text-red-500 text-sm">{error.word}</span>}
            </div>
            {isRemovable && (
                <button
                    type="button"
                    onClick={onRemove}
                    className="text-red-500 hover:text-red-700 mt-6"
                >
                    Remove
                </button>
            )}
        </div>
    );
};

export default RuleInput;
