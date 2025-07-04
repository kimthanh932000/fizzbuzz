import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { createGame } from '../api/game';
import type { GameDto, RuleDto, ValidationError } from '../types';
import RuleInput from '../components/RuleInput';
import axios from 'axios';

const CreateGamePage = () => {
  const [game, setGame] = useState<GameDto>({
    name: '',
    authorName: '',
    range: 1,
    durationInSeconds: 1,
    rules: [{ divisibleBy: 0, word: '' }],
  });

  const [error, setError] = useState<ValidationError>({});
  const [generalError, setGeneralError] = useState('');
  const [loading, setLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const updateField = (key: keyof GameDto, value: number | string) => {
    setGame(prev => ({ ...prev, [key]: value }));
  };

  const updateRule = (index: number, updatedRule: RuleDto) => {
    const updatedRules = [...game.rules];
    updatedRules[index] = updatedRule;
    setGame(prev => ({ ...prev, rules: updatedRules }));
  };

  const addRule = () => {
    setGame(prev => ({ ...prev, rules: [...prev.rules, { divisibleBy: 0, word: '' }] }));
  };

  const removeRule = (index: number) => {
    const updatedRules = [...game.rules];
    updatedRules.splice(index, 1);
    setGame(prev => ({ ...prev, rules: updatedRules }));
  };

  const mapErrorsToState = (apiErrors: Record<string, string>): ValidationError => {
    const errors: ValidationError = {};

    Object.entries(apiErrors).forEach(([key, value]) => {
      const match = key.match(/^Rules\[(\d+)\]\.(\w+)/);
      if (match) {
        const index = Number(match[1]);
        const field = match[2][0].toLowerCase() + match[2].slice(1);

        if (!errors.rules) {
          errors.rules = []
        };
        if (!errors.rules[index]) {
          errors.rules[index] = {}
        };
        errors.rules[index][field] = value;
      } else {
        const localKey = key[0].toLowerCase() + key.slice(1);
        errors[localKey] = value;
      }
    });

    return errors;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      setLoading(true);
      await createGame(game);
      navigate('/');
    } catch (err) {
      if (axios.isAxiosError(err)) {
        const apiErrors = mapErrorsToState(err.response?.data?.errors);
        console.error('API Errors:', apiErrors);
        setError(apiErrors);
      } else {
        setGeneralError('Unexpected error occurred.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-xl mx-auto mt-8 px-4">
      <h1 className="text-3xl font-bold mb-6">Create New Game</h1>

      {generalError && <p className="text-red-500 mb-4">{generalError}</p>}

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block font-medium">Game Name</label>
          <input
            type="text"
            value={game.name}
            onChange={(e) => updateField('name', e.target.value)}
            className="w-full border px-3 py-2 rounded"
          />
          {error.name && <p className="text-red-500 text-sm">{error.name}</p>}
        </div>

        <div>
          <label className="block font-medium">Author Name</label>
          <input
            type="text"
            value={game.authorName}
            onChange={(e) => updateField('authorName', e.target.value)}
            className="w-full border px-3 py-2 rounded"
          />
          {error.authorName && <p className="text-red-500 text-sm">{error.authorName}</p>}
        </div>

        <div>
          <label className="block font-medium">Range</label>
          <input
            type="number"
            value={game.range}
            onChange={(e) => updateField('range', Number(e.target.value))}
            className="w-full border px-3 py-2 rounded"
            min={1}
          />
          {error.range && <p className="text-red-500 text-sm">{error.range}</p>}
        </div>

        <div>
          <label className="block font-medium">Duration (seconds)</label>
          <input
            type="number"
            value={game.durationInSeconds}
            onChange={(e) => updateField('durationInSeconds', Number(e.target.value))}
            className="w-full border px-3 py-2 rounded"
            min={1}
          />
          {error.durationInSeconds && (
            <p className="text-red-500 text-sm">{error.durationInSeconds}</p>
          )}
        </div>

        <div>
          <label className="block font-medium mb-2">Rules</label>
          {game.rules.map((rule, index) => (
            <RuleInput
              key={index}
              rule={rule}
              onChange={(updatedRule) => updateRule(index, updatedRule)}
              onRemove={() => removeRule(index)}
              isRemovable={game.rules.length > 1}
              error={error.rules?.[index]}
            />
          ))}
          <button type="button" onClick={addRule} className="mt-2 text-blue-600 hover:underline">
            + Add Rule
          </button>
        </div>

        <button
          type="submit"
          className="w-full py-2 bg-blue-600 hover:bg-blue-700 text-white font-semibold rounded disabled:opacity-50"
          disabled={loading}
        >
          {loading ? 'Creating...' : 'Create Game'}
        </button>
      </form>
    </div>
  );
};

export default CreateGamePage;
